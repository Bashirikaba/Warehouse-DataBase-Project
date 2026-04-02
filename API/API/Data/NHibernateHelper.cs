using API.Mappings;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace API.Data;

public static class NHibernateHelper
{
    private static ISessionFactory _sessionFactory;

    public static ISessionFactory SessionFactory
    {
        get
        {
            if (_sessionFactory == null)
                _sessionFactory = CreateSessionFactory();
            return _sessionFactory;
        }
    }

    private static ISessionFactory CreateSessionFactory()
    {
        var config = new Configuration();

        config.DataBaseIntegration(db =>
        {
            db.Driver<NpgsqlDriver>();
            db.Dialect<PostgreSQLDialect>();
            db.ConnectionString = "Host=localhost:5432;Database=warehouse;Username=postgres;Password=TestConn1";
            db.LogSqlInConsole = true;
            db.LogFormattedSql = true;
        });

        var mapper = new ModelMapper();

        mapper.AddMappings(typeof(PositionMap).Assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract &&
                        t.BaseType != null &&
                        t.BaseType.IsGenericType &&
                        t.BaseType.GetGenericTypeDefinition() == typeof(ClassMapping<>)));

        var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
        config.AddMapping(mapping);

        return config.BuildSessionFactory();
    }
}
