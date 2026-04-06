namespace API.AutoRouteProvider;

// TODO узнать зачем нужны булева
// TODO сделать чтоб автоматически подбиралось название сервиса в маршруте
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class AutoRouteAttribute : Attribute
{
    public string? RoutePrefix { get; set; }
}
