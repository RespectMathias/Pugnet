using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using Pugnet.Interfaces;
using Pugnet.Extensions;
using Jering.Javascript.NodeJS;

namespace Pugnet.Tests
{
    [Trait("Category", "MvcBuilderExtensions")]
    public class MvcBuilderExtensionsTests : IClassFixture<MvcBuilderExtensionsTestsFixture>
    {
        public MvcBuilderExtensionsTestsFixture Fixture;

        public MvcBuilderExtensionsTests(MvcBuilderExtensionsTestsFixture fixture)
        {
            Fixture = fixture;
        }

        public static IEnumerable<object[]> NeededServices =
            new List<object[]>
            {
                new []{ typeof(INodeJSService) },
                new []{ typeof(IPugRendering) },
                new []{ typeof(IPugnetViewEngine) }
            };

        [Theory]
        [MemberData(nameof(NeededServices))]
        public void MvcBuilderExtensions_AddPugnet_AddsNeededServices(Type neededService)
        {
            Assert.Contains(neededService, Fixture.Services);
        }

    }

    public class MvcBuilderExtensionsTestsFixture
    {
        public IEnumerable<Type> Services { get; }

        public MvcBuilderExtensionsTestsFixture()
        {
            var mvcBuilder = new MvcBuilder(new ServiceCollection(), new ApplicationPartManager());
            mvcBuilder.AddPugnet();
            Services = mvcBuilder.Services.Select(s => s.ServiceType).ToList();
        }
    }
}
