using System.Reflection;
using Data.Helpers;
using Data.Classes;
using Data.Interfaces;
using API.AutoRouteProvider;

var builder = WebApplication.CreateBuilder(args);

var assemblies = new[] { Assembly.GetExecutingAssembly() };
var serviceTypes = assemblies
    .SelectMany(a => a.GetTypes())
    .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttribute<AutoRouteAttribute>() != null)
    .ToList();

foreach (var serviceType in serviceTypes)
{
    builder.Services.AddScoped(serviceType);
    foreach (var iface in serviceType.GetInterfaces())
    {
        builder.Services.AddScoped(iface, serviceType);
    }
}

Registry.Build(assemblies);

builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSingleton(NHibernateHelper.SessionFactory);
builder.Services.AddScoped(sp => NHibernateHelper.OpenSession());
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// TODO наверное useRouting не нужен
app.UseRouting();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

// TODO Посмотреть как можно более красиво добавить маппинги в сборку