using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Business.Mappings;

namespace ApplicationData.Shared.Helpers;

public static class NHibernateHelper
{
    private static ISessionFactory? _sessionFactory;

    public static ISessionFactory SessionFactory
    {
        get
        {
            _sessionFactory ??= CreateSessionFactory();
            return _sessionFactory;
        }
    }

    private static ISessionFactory CreateSessionFactory()
    {
        Configuration config = new();

        config.DataBaseIntegration(db =>
        {
            db.Driver<NpgsqlDriver>();
            db.Dialect<PostgreSQLDialect>();
            db.ConnectionString = "Host=localhost:5432;Database=warehouse;Username=postgres;Password=TestConn1";
            db.LogSqlInConsole = true;
            db.LogFormattedSql = true;
        });

        ModelMapper mapper = new();

        Assembly mappingsAssembly = Assembly.GetAssembly(typeof(PositionMap))!;

        mapper.AddMappings(mappingsAssembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract &&
                        t.BaseType != null &&
                        t.BaseType.IsGenericType &&
                        t.BaseType.GetGenericTypeDefinition() == typeof(ClassMapping<>)));

        HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
        config.AddMapping(mapping);

        return config.BuildSessionFactory();
    }

    public static ISession OpenSession()
    {
        return SessionFactory.OpenSession();
    }
}
