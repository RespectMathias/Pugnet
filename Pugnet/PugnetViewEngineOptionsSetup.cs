using Microsoft.Extensions.Options;

using static Pugnet.Constants;

namespace Pugnet;

public class PugnetViewEngineOptionsSetup : ConfigureOptions<PugnetViewEngineOptions>
{
    public PugnetViewEngineOptionsSetup() : base(Configure) { }

    private new static void Configure(PugnetViewEngineOptions options)
    {
        options.ViewLocationFormats.Add("Views/{1}/{0}" + VIEW_EXTENSION);
        options.ViewLocationFormats.Add("Views/Shared/{0}" + VIEW_EXTENSION);
    }
}
