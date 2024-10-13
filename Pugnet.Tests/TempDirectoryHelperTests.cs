using Pugnet.Helpers;

namespace Pugnet.Tests;

[Trait("Category", "Helper")]
public class TempDirectoryHelperTests
{
    [Fact]
    public void Directory_Created_Exists()
    {
        var path = TemporaryDirectoryHelper.CreateTemporaryDirectory();
        var result = Directory.Exists(path);
        Assert.True(result);
    }
}
