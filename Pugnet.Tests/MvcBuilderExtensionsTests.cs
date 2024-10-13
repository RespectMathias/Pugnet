using Jering.Javascript.NodeJS;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using Pugnet.Interfaces;

namespace Pugnet.Tests
{
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
        public void MvcBuilderExtensions_AddPug_AddsNeededServices(Type neededService)
        {
            Assert.Contains(neededService, fixture.Services);
        }
    }

    public class MvcBuilderExtensionsTestsFixture
    {
        public IEnumerable<Type> Services { get; }

        public MvcBuilderExtensionsTestsFixture()
        {
            var mvcBuilder = new MvcBuilder(new ServiceCollection(), new ApplicationPartManager());
            _ = mvcBuilder.AddPug();
            Services = mvcBuilder.Services.Select(s => s.ServiceType).ToList();
        }
    }
}
