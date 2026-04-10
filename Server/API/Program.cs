using API.AutoRouteProvider;
using ApplicationData.Shared;
using ApplicationData.Shared.Helpers;
using ApplicationData.Infrastructure;
using Business.Attributes;
using Services.Shared;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

Assembly servicesAssembly = Assembly.GetAssembly(typeof(PositionsService))!;
List<Type> servicesTypes = servicesAssembly.GetTypes()
    .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttribute<AutoRouteAttribute>() != null)
    .ToList();

foreach (Type serviceType in servicesTypes)
{
    builder.Services.AddScoped(serviceType);
    foreach (Type iface in serviceType.GetInterfaces())
    {
        builder.Services.AddScoped(iface, serviceType);
    }
}

AutoRouteRegistry.Build(servicesTypes);

builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Services.AddControllers();
builder.Services.AddSingleton(NHibernateHelper.SessionFactory);
builder.Services.AddScoped(sp => NHibernateHelper.OpenSession());
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IReadOnlyRepository<>), typeof(ReadOnlyRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();