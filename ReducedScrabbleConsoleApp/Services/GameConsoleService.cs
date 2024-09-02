namespace ReducedScrabbleConsoleApp.Services;

using ReducedScrabbleConsoleApp.Models;
using ReducedScrabbleConsoleApp.Repository;

internal sealed class GameConsoleService
{
    private bool _isRunning = true;
    private readonly ISessionService _sessionService;

    private static readonly GameConsoleService _instance = new GameConsoleService();
    public static GameConsoleService Instance => _instance;
    private GameConsoleService(ISessionService sessionService) 
    {
        _sessionService = sessionService;
    }

    private GameConsoleService() : this(new SessionService(new WordsRepository()))
    {
    }

    public void Run()
    {
        while (_isRunning)
        {
            string input = Console.ReadLine();
            if (input == "c-exit") 
            {
                _isRunning = false;
            }
            else if(input == "c-reset")
            {
                _sessionService.Reset();
            }
            else if(input == "c-stats")
            {
                GameStatisticsDto currentStatistics = _sessionService.GetCurrentStatistics();
                Console.WriteLine($@"Longest word used: {currentStatistics.LongestWord}
Highest points word: {currentStatistics.HighestPointsWord}
Points number: {currentStatistics.PointsNumber}");
            }
            else
            {
                InputResultDto result = _sessionService.ProcessInputWord(input);
                string acceptedStatus = result.WordAccepted ? "accepted." : "rejected.";
                string suffixMessage = result.WordAccepted
                    ? $"{result.WordPoints} points."
                    : result.InvalidLetters.Length > 0
                        ? $"Letters invalid (not supported): {string.Join(",", result.InvalidLetters)}."
                        : result.WordExists 
                            ? $"Letters not available: {string.Join(",", result.NotAvailableLetters)}."
                            : "Word does not exist.";
                Console.WriteLine($"{result.InputWordEvaluated} {acceptedStatus} {suffixMessage}");
                //the ternary operator got too complex and should be refactored into normal if-else statements, but I stop here! :)
            }
        }

        Console.WriteLine("GAME OVER! :)");
    }
}
