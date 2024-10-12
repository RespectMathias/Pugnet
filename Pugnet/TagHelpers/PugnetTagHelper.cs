using Microsoft.AspNetCore.Razor.TagHelpers;
using Pugnet.Interfaces;

namespace Pugnet.TagHelpers
{
    [HtmlTargetElement("Pugnet")]
    public class PugnetTagHelper : TagHelper
    {
        public object Model { get; set; }
        public string View { get; set; }

        private readonly IPugRendering _pugRendering;

        public PugnetTagHelper(IPugRendering pugRendering)
        {
            _pugRendering = pugRendering;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var result = await _pugRendering.Render(new FileInfo(View), Model, null, null).ConfigureAwait(false);
            output.TagName = null;
            output.Content.AppendHtml(result);
        }
    }
}
