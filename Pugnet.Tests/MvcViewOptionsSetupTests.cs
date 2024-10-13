using Microsoft.Extensions.Options;
using Pugnet.Options;
using Pugnet.ViewEngines;

namespace Pugnet.Tests;

[Trait("Category", "MvcViewOptionsSetup")]
public class MvcViewOptionsSetupTests
{
    [Fact]
    public void MvcViewOptionsSetup_ConfigureWithNullPugViewEngine_ThrowsArgumentNullException()
    {
        var options = new MvcViewOptionsSetup(null!);
        _ = Assert.Throws<ArgumentNullException>(() => options.Configure(new MvcViewOptions()));
    }

    [Fact]
    public void MvcViewOptionsSetup_ConfigureWithNullOptions_ThrowsArgumentNullException()
    {
        var viewEngine = new PugViewEngine(null!, new OptionsWrapper<ViewEngineOptions>(new ViewEngineOptions()));
        var options = new MvcViewOptionsSetup(viewEngine);
        _ = Assert.Throws<ArgumentNullException>(() => options.Configure(null!));
    }
}
