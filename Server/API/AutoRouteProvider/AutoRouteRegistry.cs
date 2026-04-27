using System.Collections.Concurrent;
using System.Reflection;

namespace API.AutoRouteProvider;

public record ServiceEntry(Type Type, ConcurrentDictionary<string, MethodEntry> Methods);

public record MethodEntry(MethodInfo Method, ParameterInfo[] Parameters);

public static class AutoRouteRegistry
{
    private static readonly ConcurrentDictionary<string, ServiceEntry> _services = new();

    public static void Build(List<Type> servicesTypes)
    {
        foreach (var type in servicesTypes)
        {
            var serviceName = type.Name.Replace("Service", "").ToLowerInvariant();

            var methods = new ConcurrentDictionary<string, MethodEntry>();

            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
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

