using Microsoft.Extensions.Options;
using Pugnet.Interfaces;

namespace Pugnet;

public class PugnetMvcViewOptionsSetup : IConfigureOptions<MvcViewOptions>
{
    private readonly IPugnetViewEngine _PugnetViewEngine;

    /// <summary>
    /// Initializes a new instance of <see cref="PugnetMvcViewOptionsSetup"/>.
    /// </summary>
    /// <param name="PugnetViewEngine">The <see cref="IPugnetViewEngine"/>.</param>
    public PugnetMvcViewOptionsSetup(IPugnetViewEngine PugnetViewEngine)
    {
        _PugnetViewEngine = PugnetViewEngine ?? throw new ArgumentNullException(nameof(PugnetViewEngine));
    }

    /// <summary>
    /// Configures <paramref name="options"/> to use <see cref="PugnetViewEngine"/>.
    /// </summary>
    /// <param name="options">The <see cref="MvcViewOptions"/> to configure.</param>
    public void Configure(MvcViewOptions options)
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        options.ViewEngines.Add(_PugnetViewEngine);
    }

}
