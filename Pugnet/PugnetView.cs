using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pugnet.Interfaces;

namespace Pugnet;

public class PugnetView(string path, IPugRendering pugRendering) : IView
{
    public string Path => path;

    public async Task RenderAsync(ViewContext context)
    {
        var result = await pugRendering.Render(new FileInfo(Path), context.ViewData.Model, context.ViewData, context.ModelState).ConfigureAwait(false);
        await context.Writer.WriteAsync(result);
    }
}
