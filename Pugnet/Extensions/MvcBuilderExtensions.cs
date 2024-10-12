using Jering.Javascript.NodeJS;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Pugnet.Interfaces;

namespace Pugnet.Extensions;

public static class MvcBuilderExtensions
{
    public static IMvcBuilder AddPugnet(this IMvcBuilder builder, Action<PugnetViewEngineOptions> setupAction = null)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.Services.AddOptions()
                        .AddTransient<IConfigureOptions<PugnetViewEngineOptions>, PugnetViewEngineOptionsSetup>();

        if (setupAction != null)
        {
            builder.Services.Configure(setupAction);
        }

        builder.Services
            .AddTransient<IConfigureOptions<MvcViewOptions>, PugnetMvcViewOptionsSetup>()
            .AddSingleton<IPugRendering, PugRendering>()
            .AddSingleton<IPugnetViewEngine, PugnetViewEngine>()
            .AddNodeJS();

        return builder;
    }
}
