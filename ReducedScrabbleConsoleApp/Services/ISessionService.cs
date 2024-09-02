namespace ReducedScrabbleConsoleApp.Services;

using ReducedScrabbleConsoleApp.Models;

public interface ISessionService
{
    void Reset();
    GameStatisticsDto GetCurrentStatistics();
    InputResultDto ProcessInputWord(string word);
}
