namespace TestProject.IntegrationTests;

using ReducedScrabbleConsoleApp.Repository;

public class WordsRepositoryTests
{
    [Fact]
    public void Word_DoesNotExist_Returns_False()
    {
        var sut = new WordsRepository();
        bool wordExists = sut.Exists("SOME FAKE WORD");
        Assert.False(wordExists);
    }

    [Fact]
    public void Word_Exists_Returns_True()
    {
        var sut = new WordsRepository();
        bool wordExists = sut.Exists("ZYZZYVAS");
        Assert.True(wordExists);
    }
}
