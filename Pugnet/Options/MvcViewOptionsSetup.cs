using Microsoft.Extensions.Options;
using Pugnet.Interfaces;
using Pugnet.ViewEngines;

namespace Pugnet.Options;

/// <summary> Initializes a new instance of <see cref="MvcViewOptionsSetup"/>. </summary>
/// <param name="pugViewEngine">The <see cref="IPugViewEngine"/>.</param>
public class MvcViewOptionsSetup(IPugViewEngine pugViewEngine) : IConfigureOptions<MvcViewOptions>
{
    /// <summary> Configures <paramref name="options"/> to use <see cref="PugViewEngine"/>. </summary>
    /// <param name="options">The <see cref="MvcViewOptions"/> to configure.</param>
    public void Configure(MvcViewOptions options)
    {
        ArgumentNullException.ThrowIfNull(pugViewEngine);
        ArgumentNullException.ThrowIfNull(options);

        options.ViewEngines.Add(pugViewEngine);
    }
}
