using Moq;
using ReducedScrabbleConsoleApp.Models;
using ReducedScrabbleConsoleApp.Repository;
using ReducedScrabbleConsoleApp.Services;

namespace TestProject.UnitTests;

public class SessionServiceUnitTests
{
    private readonly Mock<IWordsRepository> _wordsRepositoryMock;
    private readonly SessionService _sut;

    public SessionServiceUnitTests()
    {
        _wordsRepositoryMock = new Mock<IWordsRepository>();
        _sut = new SessionService(_wordsRepositoryMock.Object);
    }

    [Fact]
    public void InvalidCharacters_Input_SpaceDetected_Fail()
    {
        const string InvalidCharsWord = "invalid word with spaces";
        InputResultDto result = _sut.ProcessInputWord(InvalidCharsWord);

        Assert.False(result.WordAccepted);
        Assert.True(result.InputWordEvaluated == InvalidCharsWord.ToUpperInvariant());
        Assert.True(result.InvalidLetters.SequenceEqual([' ']));
    }

    [Fact]
    public void NotExistingWord_ReturnsFalse()
    {
        const string FakeWord = "DOESNOTEXIST";
        _wordsRepositoryMock.Setup(x => x.Exists(FakeWord)).Returns(false);

        InputResultDto result = _sut.ProcessInputWord(FakeWord);

        Assert.False(result.WordExists);
        Assert.False(result.WordAccepted);
    }

    [Fact]
    public void ExistingWord_AvailableLetters_ReturnsTrueAceptedCalculated()
    {
        const string FakeWord = "JOKE";
        _wordsRepositoryMock.Setup(x => x.Exists(FakeWord)).Returns(true);

        InputResultDto result = _sut.ProcessInputWord(FakeWord);

        Assert.True(result.WordExists);
        Assert.True(result.WordAccepted);
        Assert.True(result.WordPoints == 15);
    }

    [Fact]
    public void ExistingWord_AvailableLetters_UpdateStatistics()
    {
        const string FakeWord = "JOKE";
        _wordsRepositoryMock.Setup(x => x.Exists(FakeWord)).Returns(true);

        InputResultDto result = _sut.ProcessInputWord(FakeWord);

        GameStatisticsDto stats = _sut.GetCurrentStatistics();
        Assert.True(stats.HighestPointsWord == FakeWord);
        Assert.True(stats.LongestWord == FakeWord);
        Assert.True(stats.PointsNumber == 15);
    }

    [Fact]
    public void Reset_ZeroesStatistics_Success()
    {
        //Arrange
        const string FakeWord = "JOKE";
        _wordsRepositoryMock.Setup(x => x.Exists(FakeWord)).Returns(true);

        InputResultDto result = _sut.ProcessInputWord(FakeWord);

        GameStatisticsDto stats = _sut.GetCurrentStatistics();
        Assert.True(stats.HighestPointsWord != string.Empty);
        Assert.True(stats.LongestWord != string.Empty);
        Assert.True(stats.PointsNumber > 0);

        //Act
        _sut.Reset();

        stats = _sut.GetCurrentStatistics();

        //Assert
        Assert.True(stats.LongestWord == string.Empty);
        Assert.True(stats.HighestPointsWord == string.Empty);
        Assert.True(stats.PointsNumber == 0);
    }

    [Fact]
    public void ExistingWord_NotAvailableLetters_ReturnsFalse_WithWrongLettersInformation()
    {
        const string FakeWord1 = "JOKE";
        const string FakeWord2 = "JAZZ";
        _wordsRepositoryMock.Setup(x => x.Exists(FakeWord1)).Returns(true);
        _wordsRepositoryMock.Setup(x => x.Exists(FakeWord2)).Returns(true);

        _ = _sut.ProcessInputWord(FakeWord1);
        InputResultDto result = _sut.ProcessInputWord(FakeWord2);

        Assert.True(result.WordExists);
        Assert.False(result.WordAccepted);
        Assert.True(result.InputWordEvaluated == FakeWord2);
        Assert.True(result.NotAvailableLetters.SequenceEqual(['J', 'Z']));
    }
}