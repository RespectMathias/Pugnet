using Jering.Javascript.NodeJS;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using Pugnet.Helpers;
using Pugnet.Interfaces;

namespace Pugnet.Tests;

[Trait("Category", "Rendering")]
public class PugRenderingTests : IClassFixture<PugRenderingTestsFixture>
{
    public static IEnumerable<object[]> Names =>
        new List<object[]>
        {
            new object[] { "dpaquette" },
            new object[] { "stimms" },
            new object[] { "MisterJames" }
        };

    private readonly PugRenderingTestsFixture _fixture;

    public PugRenderingTests(PugRenderingTestsFixture fixture)
    {
        _fixture = fixture;
    }

    private async Task<string> RenderView(
    string viewName,
    string pugContent,
    object model = null,
    ViewDataDictionary viewData = null,
    ModelStateDictionary modelState = null)
    {
        // Write the pug content to a temporary file
        var pugFilePath = Path.Combine(_fixture.TestPugDirectory, viewName);
        await File.WriteAllTextAsync(pugFilePath, pugContent).ConfigureAwait(false);

        return await _fixture.Renderer
            .Render(new FileInfo(pugFilePath), model, viewData, modelState)
            .ConfigureAwait(false);
    }

    [Fact]
    public async Task ViewEngine_View_Renders()
    {
        var renderResult = await RenderView("empty.pug", string.Empty, new { }).ConfigureAwait(false);
        Assert.Equal(string.Empty, renderResult);
    }

    [Fact]
    public async Task ViewEngine_NullModel_Handled()
    {
        var renderResult = await RenderView("empty.pug", string.Empty).ConfigureAwait(false);
        Assert.Equal(string.Empty, renderResult);
    }

    [Theory]
    [MemberData(nameof(Names))]
    public async Task ViewEngine_ModelValue_Embedded(string name)
    {
        // Pug template that uses a model property 'firstName'
        string pugContent = "p= firstName";

        var renderResult = await RenderView(
            "simpleName.pug",
            pugContent,
            new { firstName = name }
        ).ConfigureAwait(false);

        Assert.Equal($"<p>{name}</p>", renderResult);
    }

    [Theory]
    [MemberData(nameof(Names))]
    public async Task ViewEngine_ViewDataValue_Embedded(string name)
    {
        // Pug template that uses a model property 'name'
        string pugContent = "p= name";

        // Include 'name' in the model
        var model = new { name };

        var renderResult = await RenderView(
            "viewData.pug",
            pugContent,
            model: model
        ).ConfigureAwait(false);

        Assert.Equal($"<p>{name}</p>", renderResult);
    }

    [Theory]
    [MemberData(nameof(Names))]
    public async Task ViewEngine_ModelStateValue_Embedded(string name)
    {
        // Extract the error message from ModelState
        var modelState = new ModelStateDictionary();
        var testString = $"{name} is an invalid value";
        modelState.AddModelError("testError", testString);

        // Get the error message
        var errorMessage = modelState["testError"].Errors[0].ErrorMessage;

        // Include 'errorMessage' in the model
        var model = new { errorMessage };

        // Pug template that uses 'errorMessage'
        string pugContent = "p= errorMessage";

        var renderResult = await RenderView(
            "modelState.pug",
            pugContent,
            model: model
        ).ConfigureAwait(false);

        Debug.WriteLine(renderResult);
        Assert.Equal($"<p>{testString}</p>", renderResult);
    }
}

public class PugRenderingTestsFixture
{
    public IPugRendering Renderer { get; }
    public string TestPugDirectory { get; }

    public PugRenderingTestsFixture()
    {
        // Set the TEMP environment variable to a directory within the project directory
        var tempDir = Path.Combine(Directory.GetCurrentDirectory(), "Temp");
        Directory.CreateDirectory(tempDir);
        Environment.SetEnvironmentVariable("TEMP", tempDir);

        // Mock the options for PugnetViewEngineOptions
        var optionsMock = new Mock<IOptions<PugnetViewEngineOptions>>();
        optionsMock.SetupGet(q => q.Value)
            .Returns(new PugnetViewEngineOptions
            {
                Pretty = false
            });

        // Create a temporary directory for test pug files
        TestPugDirectory = TemporaryDirectoryHelper.CreateTemporaryDirectory();

        // Set up the service collection
        var services = new ServiceCollection();

        // Add NodeJS
        services.AddNodeJS();

        // Build the service provider
        var serviceProvider = services.BuildServiceProvider();

        // Get the INodeJSService from the service provider
        var nodeJSService = serviceProvider.GetRequiredService<INodeJSService>();

        // Instantiate the renderer with the nodeJSService
        Renderer = new PugRendering(nodeJSService, optionsMock.Object);
    }
}
