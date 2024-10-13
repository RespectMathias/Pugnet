namespace Pugnet.Options;

public class ViewEngineOptions
{
    public IList<string> ViewLocationFormats { get; } = [];
    public string? BaseDir { get; set; }
    public bool Pretty { get; set; }
}
