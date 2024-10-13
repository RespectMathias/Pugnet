using Microsoft.AspNetCore.Razor.TagHelpers;
using Pugnet.Interfaces;

namespace Pugnet.Helpers
{
    [HtmlTargetElement("Pugnet")]
    public class PugTagHelper(IPugRenderer pugRenderer) : TagHelper
    {
        public object? Model { get; set; }
        public string? View { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var result = await pugRenderer.Render(new FileInfo(View!), Model!, null!, null!).ConfigureAwait(false);
            output.TagName = null;
            _ = output.Content.AppendHtml(result);
        }
    }
}
