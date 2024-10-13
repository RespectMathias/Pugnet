using Jering.Javascript.NodeJS;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Pugnet.Interfaces;
using Pugnet.Helpers;
using Pugnet.Options;

namespace Pugnet.Rendering;

public class PugRenderer : IPugRenderer
{
    private readonly INodeJSService _nodeJSService;
    private readonly ViewEngineOptions _options;
    private readonly string _tempDirectory;

    public PugRenderer(INodeJSService nodeJSService, IOptions<ViewEngineOptions> options)
    {
        _nodeJSService = nodeJSService;
        _tempDirectory = TemporaryDirectoryHelper.CreateTemporaryDirectory();
        EmbeddedFileHelper.ExpandEmbeddedFiles(_tempDirectory);
        _options = options.Value;
    }

    public async Task<string> Render(FileInfo pugFile, object model, ViewDataDictionary viewData, ModelStateDictionary modelState)
    {
        var opts = new
        {
            pretty = _options.Pretty ? "\t" : null,
            basedir = _options.BaseDir
        };

        var modulePath = Path.Combine(_tempDirectory, "pugcompile.js");

        return await _nodeJSService
            .InvokeFromFileAsync<string>(
                modulePath: modulePath,
                args: [pugFile.FullName, model, viewData, modelState, opts]
            )
            .ConfigureAwait(false) ?? string.Empty;
    }
}
