using System.Collections.Concurrent;
using System.Reflection;

namespace API.AutoRouteProvider;
// TODO везде убрать var
// TODO может надо интерфейсы я хз
public static class Registry
{
    private static readonly ConcurrentDictionary<string, ServiceEntry> _services = new();

    public static void Build(params Assembly[] assemblies)
    {
        // TODO проверить, нужен ли selectMany
        // TODO посмотреть в дебаге, что вообще мы получаем из ассембли
        // TODO таким же способом можно добавить маппинги
        var types = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttribute<AutoRouteAttribute>() != null);

        foreach (var type in types)
        {
            var attr = type.GetCustomAttribute<AutoRouteAttribute>();
            // TODO возможно, роутпрефикс надо будет убрать
            var serviceName = (attr?.RoutePrefix ?? type.Name.Replace("Service", "")).ToLowerInvariant();

            var methods = new ConcurrentDictionary<string, MethodEntry>();

            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                // TODO посмотреть что за спешиал нейм
                if (method.IsSpecialName) continue;
                methods[method.Name] = new MethodEntry(method, method.GetParameters());
            }

            _services[serviceName] = new ServiceEntry(type, methods);
        }
    }

    public static bool TryGetService(string serviceName, out ServiceEntry? entry)
    {
        return _services.TryGetValue(serviceName.ToLowerInvariant(), out entry);
    }
}

public record ServiceEntry(Type Type, ConcurrentDictionary<string, MethodEntry> Methods);

public record MethodEntry(MethodInfo Method, ParameterInfo[] Parameters);