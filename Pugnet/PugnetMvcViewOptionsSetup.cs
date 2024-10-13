using Microsoft.Extensions.Options;
using Pugnet.Interfaces;

namespace Pugnet;

/// <summary> Initializes a new instance of <see cref="PugnetMvcViewOptionsSetup"/>. </summary>
/// <param name="PugnetViewEngine">The <see cref="IPugnetViewEngine"/>.</param>
public class PugnetMvcViewOptionsSetup(IPugnetViewEngine PugnetViewEngine) : IConfigureOptions<MvcViewOptions>
{
    private readonly IPugnetViewEngine _pugnetViewEngine = PugnetViewEngine ?? throw new ArgumentNullException(nameof(PugnetViewEngine));

    /// <summary> Configures <paramref name="options"/> to use <see cref="PugnetViewEngine"/>. </summary>
    /// <param name="options">The <see cref="MvcViewOptions"/> to configure.</param>
    public void Configure(MvcViewOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        options.ViewEngines.Add(_pugnetViewEngine);
    }
}
