using Microsoft.AspNetCore.Razor.TagHelpers;
using Pugnet.Interfaces;

namespace Pugnet.TagHelpers
{
    [HtmlTargetElement("Pugnet")]
    public class PugnetTagHelper(IPugRendering pugRendering) : TagHelper
    {
        public object? Model { get; set; }
        public string? View { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var result = await pugRendering.Render(new FileInfo(View!), Model!, null!, null!).ConfigureAwait(false);
            output.TagName = null;
            _ = output.Content.AppendHtml(result);
        }
    }
}
