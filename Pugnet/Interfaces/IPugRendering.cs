using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Pugnet.Interfaces;

public interface IPugRendering
{
    Task<string> Render(FileInfo pugFile, object model, ViewDataDictionary viewData, ModelStateDictionary modelState);
}
