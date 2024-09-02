namespace ReducedScrabbleConsoleApp.Services;

using ReducedScrabbleConsoleApp.Models;
using ReducedScrabbleConsoleApp.Repository;

internal sealed class GameConsoleService
{
    private bool _isRunning = true;
    private readonly ISessionService _sessionService;

    private static readonly GameConsoleService _instance = new();
    public static GameConsoleService Instance => _instance;
    private GameConsoleService(ISessionService sessionService) 
    {
        _sessionService = sessionService;
    }

    private GameConsoleService() : this(new SessionService(new WordsRepository()))
    {
        //this singleton should be registered and set with a proper lifetime policy via a DI container
    }

    public void Run()
    {
        while (_isRunning)
        {
            string input = Console.ReadLine();
            switch (input)
            {
                case "c-exit":
                    _isRunning = false;
                    break;
                case "c-reset":
                    _sessionService.Reset();
                    break;
                case "c-stats":
                {
                    GameStatisticsDto currentStatistics = _sessionService.GetCurrentStatistics();
                    Console.WriteLine($@"Longest word used: {currentStatistics.LongestWord}
Highest points word: {currentStatistics.HighestPointsWord}
Points number: {currentStatistics.PointsNumber}");
                    break;
                }
                default:
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
                    //the ternary operator got too complex and should be refactored into normal if-else statements, or a Builder class, but I stop here! :)
                    Console.WriteLine($"{result.InputWordEvaluated} {acceptedStatus} {suffixMessage}");
                    break;
                }
            }
        }

        Console.WriteLine("GAME OVER! :)");
    }
}
