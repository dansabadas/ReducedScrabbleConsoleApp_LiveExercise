namespace TestProject.IntegrationTests;

using ReducedScrabbleConsoleApp.Infrastructure;

public class FileStringReaderTests
{
    [Fact]
    public void ReadFile_AllDictionary_EmbeddedResource_Success()
    {
        var result = FileStringReader.ReadFile();
        Assert.True(result.Length == 178691);
    }
}