using System.Reflection;
using System.Text.Json;
using API.AutoRouteProvider;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GatewayController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;

    public GatewayController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [HttpPost("{serviceName}/{methodName}")]
    public async Task<IActionResult> Action(string serviceName, string methodName)
    {
        if (!Registry.TryGetService(serviceName, out var serviceEntry))
            return NotFound($"Сервис '{serviceName}' не найден");

        if (!serviceEntry!.Methods.TryGetValue(methodName, out var methodEntry))
            return NotFound($"Метод '{methodName}' сервиса '{serviceName}' не найден");

        var serviceInstanse = _serviceProvider.GetRequiredService(serviceEntry.Type);

        object?[] parameters;
        try
        {
            parameters = await BindParametersAsync(methodEntry.Parameters);
        }
        catch (Exception e)
        {
            return BadRequest($"Неверные параметры: {e.Message}");
        }

        object? result;
        try
        {
            result = methodEntry.Method.Invoke(serviceInstanse, parameters);
            if (result is Task task)
            {
                // TODO че это
                await task.ConfigureAwait(false);
                var resultType = result.GetType();
                // TODO че тут такое происходит узнать
                if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Task<>))
                {
                    result = resultType.GetProperty("Result")?.GetValue(result);
                }
                else
                {
                    result = null;
                }
            }
        }
        catch (TargetInvocationException e)
        {
            return StatusCode(500, e.InnerException?.Message ?? e.Message);
        }

        return Ok(result);
    }

    // TODO тоже понять че происходит
    private async Task<object?[]> BindParametersAsync(ParameterInfo[] parameters)
    {
        if (parameters.Length == 0)
            return [];

        using var jsonDoc = await Request.ReadFromJsonAsync<JsonDocument>();
        if (jsonDoc == null)
            throw new ArgumentException("Тело запроса пустое или имеет неправильную структуру JSON");

        var root = jsonDoc.RootElement;
        var result = new object?[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            var param = parameters[i];
            if (root.TryGetProperty(param.Name!, out var property))
            {
                result[i] = JsonSerializer.Deserialize(property.GetRawText(), param.ParameterType);
            }
            else
            {
                if (param.HasDefaultValue)
                    result[i] = param.DefaultValue;
                else if (param.ParameterType.IsValueType)
                    result[i] = Activator.CreateInstance(param.ParameterType);
                else
                    result[i] = null;
            }
        }
        return result;
    }
}
