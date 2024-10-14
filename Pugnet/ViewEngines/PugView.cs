using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pugnet.Interfaces;

namespace Pugnet.ViewEngines;

public class PugView(string path, IPugRenderer pugRenderer) : IView
{
    public string Path => path;

    public async Task RenderAsync(ViewContext context)
    {
        var result = await pugRenderer.Render(new FileInfo(Path), context.ViewData.Model!, context.ViewData, context.ModelState).ConfigureAwait(false);
        await context.Writer.WriteAsync(result);
    }
}
