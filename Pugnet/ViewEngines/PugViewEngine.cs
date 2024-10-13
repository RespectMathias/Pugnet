using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Options;
using Pugnet.Interfaces;
using Pugnet.Helpers;
using Pugnet.Options;
using static Pugnet.Constants;

namespace Pugnet.ViewEngines;

public class PugViewEngine(IPugRenderer pugRenderer, IOptions<ViewEngineOptions> optionsAccessor) : IPugViewEngine
{
    private readonly ViewEngineOptions _options = optionsAccessor?.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));

    public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
    {
        var controllerName = context.GetNormalizedRouteValue(CONTROLLER_KEY);
        _ = context.GetNormalizedRouteValue(AREA_KEY);

        var checkedLocations = new List<string>();
        foreach (var location in _options.ViewLocationFormats)
        {
            var view = string.Format(location, viewName, controllerName);
            if (File.Exists(view))
            {
                // ReSharper disable once Mvc.ViewNotResolved
                return ViewEngineResult.Found("Default", new PugView(view, pugRenderer));
            }
            checkedLocations.Add(view);
        }

        return ViewEngineResult.NotFound(viewName, checkedLocations);
    }

    public ViewEngineResult GetView(string executingFilePath, string viewPath, bool isMainPage)
    {
        var applicationRelativePath = PathHelper.GetAbsolutePath(executingFilePath, viewPath);

        if (!PathHelper.IsAbsolutePath(viewPath))
        {
            // Not a path this method can handle.
            return ViewEngineResult.NotFound(applicationRelativePath, []);
        }

        // ReSharper disable once Mvc.ViewNotResolved
        return ViewEngineResult.Found("Default", new PugView(applicationRelativePath, pugRenderer));
    }
}
