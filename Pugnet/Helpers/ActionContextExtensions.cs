namespace Pugnet.Helpers;

public static class ActionContextExtensions
{
    public static string? GetNormalizedRouteValue(this ActionContext context, string key)
    {
        ArgumentNullException.ThrowIfNull(context);

        ArgumentNullException.ThrowIfNull(key);

        if (!context.RouteData.Values.TryGetValue(key, out var routeValue))
        {
            return null!;
        }

        string normalizedValue = null!;
        if (context.ActionDescriptor.RouteValues.TryGetValue(key, out var value) && !string.IsNullOrEmpty(value))
        {
            normalizedValue = value;
        }

        var stringRouteValue = routeValue?.ToString();
        return string.Equals(normalizedValue, stringRouteValue, StringComparison.OrdinalIgnoreCase) ? normalizedValue : stringRouteValue;
    }
}
