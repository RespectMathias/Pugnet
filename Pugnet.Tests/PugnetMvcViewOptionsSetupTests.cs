using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using Moq;
using Pugnet;
using Xunit;

namespace Pugnet.Tests
{
    [Trait("Category", "PugnetMvcViewOptionsSetup")]
    public class PugnetMvcViewOptionsSetupTests
    {
        [Fact]
        public void PugnetMvcViewOptionsSetup_ConstructorWithNullViewEngine_ThrowsArgumentNullException()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new PugnetMvcViewOptionsSetup(null));
        }

        [Fact]
        public void PugnetMvcViewOptionsSetup_ConfigureWithNullOptions_ThrowsArgumentNullException()
        {
            var viewEngine = new PugnetViewEngine(null, new OptionsWrapper<PugnetViewEngineOptions>(new PugnetViewEngineOptions()));
            var options = new PugnetMvcViewOptionsSetup(viewEngine);
            Assert.Throws(typeof(ArgumentNullException), () => options.Configure(null));
        }
    }
}
