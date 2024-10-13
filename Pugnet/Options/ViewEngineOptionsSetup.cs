using Microsoft.Extensions.Options;
using static Pugnet.Constants;

namespace Pugnet.Options;

public class ViewEngineOptionsSetup : ConfigureOptions<ViewEngineOptions>
{
    public ViewEngineOptionsSetup() : base(Configure) { }

    private static new void Configure(ViewEngineOptions options)
    {
        options.ViewLocationFormats.Add("Views/{1}/{0}" + VIEW_EXTENSION);
        options.ViewLocationFormats.Add("Views/Shared/{0}" + VIEW_EXTENSION);
    }
}
