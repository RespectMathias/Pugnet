using Jering.Javascript.NodeJS;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Pugnet.Interfaces;
using Pugnet.Options;
using Pugnet.Rendering;
using Pugnet.ViewEngines;

namespace Pugnet;

public static class MvcBuilderExtensions
{
    public static IMvcBuilder AddPug(this IMvcBuilder builder, Action<ViewEngineOptions> setupAction = null!)
    {
        ArgumentNullException.ThrowIfNull(builder);

        _ = builder.Services.AddOptions()
                            .AddTransient<IConfigureOptions<ViewEngineOptions>, ViewEngineOptionsSetup>();

        if (setupAction != null)
        {
            _ = builder.Services.Configure(setupAction);
        }

        _ = builder.Services.AddTransient<IConfigureOptions<MvcViewOptions>, MvcViewOptionsSetup>()
                            .AddSingleton<IPugRenderer, PugRenderer>()
                            .AddSingleton<IPugViewEngine, PugViewEngine>()
                            .AddNodeJS();

        return builder;
    }
}
