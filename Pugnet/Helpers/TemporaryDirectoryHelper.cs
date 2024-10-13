namespace Pugnet.Helpers;

public static class TemporaryDirectoryHelper
{
    public static string CreateTemporaryDirectory()
    {
        var tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

        if (!Directory.Exists(tempDirectory))
        {
            _ = Directory.CreateDirectory(tempDirectory);
        }

        return tempDirectory;
    }
}
