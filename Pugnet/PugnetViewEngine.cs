using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Options;
using Pugnet.Interfaces;
using Pugnet.Extensions;
using Pugnet.Helpers;

using static Pugnet.Constants;

namespace Pugnet;

public class PugnetViewEngine : IPugnetViewEngine
{
    private readonly IPugRendering _pugRendering;
    private readonly PugnetViewEngineOptions _options;

    public PugnetViewEngine(IPugRendering pugRendering, IOptions<PugnetViewEngineOptions> optionsAccessor)
    {
        _options = optionsAccessor?.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));
        _pugRendering = pugRendering;
    }

    public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
    {
        var controllerName = context.GetNormalizedRouteValue(CONTROLLER_KEY);
        var areaName = context.GetNormalizedRouteValue(AREA_KEY);

        var checkedLocations = new List<string>();
        foreach (var location in _options.ViewLocationFormats)
        {
            var view = string.Format(location, viewName, controllerName);
            if (File.Exists(view))
            {
                // ReSharper disable once Mvc.ViewNotResolved
                return ViewEngineResult.Found("Default", new PugnetView(view, _pugRendering));
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
            return ViewEngineResult.NotFound(applicationRelativePath, Enumerable.Empty<string>());
        }

        // ReSharper disable once Mvc.ViewNotResolved
        return ViewEngineResult.Found("Default", new PugnetView(applicationRelativePath, _pugRendering));
    }
}
