using Microsoft.Extensions.DependencyInjection;
using Pugnet.Interfaces;
using Jering.Javascript.NodeJS;

namespace Pugnet.Tests;

[Trait("Category", "MvcBuilderExtensions")]
public class MvcBuilderExtensionsTests(MvcBuilderExtensionsTestsFixture fixture) : IClassFixture<MvcBuilderExtensionsTestsFixture>
{
    public static readonly IEnumerable<object[]> NeededServices =
        [
            [typeof(INodeJSService)],
            [typeof(IPugRenderer)],
            [typeof(IPugViewEngine)]
        ];

    [Theory]
    [MemberData(nameof(NeededServices))]
    public void AddPugnet_Registers_Needed_Services(Type neededService)
    {
        Assert.Contains(fixture.Services, s => s.ServiceType == neededService && s.Lifetime == ServiceLifetime.Singleton);
    }
}

public class MvcBuilderExtensionsTestsFixture
{
    public IServiceProvider ServiceProvider { get; }
    public List<ServiceDescriptor> Services { get; }

    public MvcBuilderExtensionsTestsFixture()
    {
        var services = new ServiceCollection();
        var mvcBuilder = services.AddControllersWithViews();

        _ = mvcBuilder.AddPug(options =>
        {
            options.BaseDir = "Views";
            options.Pretty = true;
        });

        ServiceProvider = services.BuildServiceProvider();
        Services = [.. services];
    }
}
