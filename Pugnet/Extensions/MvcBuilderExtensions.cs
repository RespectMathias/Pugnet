using Jering.Javascript.NodeJS;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Pugnet.Interfaces;

namespace Pugnet.Extensions;

public static class MvcBuilderExtensions
{
    public static IMvcBuilder AddPugnet(this IMvcBuilder builder, Action<PugnetViewEngineOptions> setupAction = null!)
    {
        ArgumentNullException.ThrowIfNull(builder);

        _ = builder.Services.AddOptions()
                            .AddTransient<IConfigureOptions<PugnetViewEngineOptions>, PugnetViewEngineOptionsSetup>();

        if (setupAction != null)
        {
            _ = builder.Services.Configure(setupAction);
        }

        _ = builder.Services.AddTransient<IConfigureOptions<MvcViewOptions>, PugnetMvcViewOptionsSetup>()
                            .AddSingleton<IPugRendering, PugRendering>()
                            .AddSingleton<IPugnetViewEngine, PugnetViewEngine>()
                            .AddNodeJS();

        return builder;
    }
}
