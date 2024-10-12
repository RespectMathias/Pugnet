namespace Pugnet;

public class PugnetViewEngineOptions
{
    public IList<string> ViewLocationFormats { get; } = new List<string>();
    public string BaseDir { get; set; }
    public bool Pretty { get; set; }
}
