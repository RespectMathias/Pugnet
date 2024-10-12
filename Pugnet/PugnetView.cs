using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pugnet.Interfaces;

namespace Pugnet;

public class PugnetView : IView
{
    private readonly string _path;
    private readonly IPugRendering _pugRendering;

    public PugnetView(string path, IPugRendering pugRendering)
    {
        _path = path;
        _pugRendering = pugRendering;
    }

    public string Path => _path;

    public async Task RenderAsync(ViewContext context)
    {
        var result = await _pugRendering.Render(new FileInfo(Path), context.ViewData.Model, context.ViewData, context.ModelState).ConfigureAwait(false);
        await context.Writer.WriteAsync(result);
    }
}
