namespace ReducedScrabbleConsoleApp.Services;

using ReducedScrabbleConsoleApp.Models;
using ReducedScrabbleConsoleApp.Repository;
using ReducedScrabbleConsoleApp.Repository.Settings;
using System.Linq;

public class SessionService : ISessionService
{
    private InternalStateMachine _internalStateMachine;

    private readonly IWordsRepository _wordsRepository;
    public SessionService(IWordsRepository wordsRepository)
    {
        _internalStateMachine = new();

        _wordsRepository = wordsRepository;
    }

    public GameStatisticsDto GetCurrentStatistics()
    {
        return new GameStatisticsDto 
        { 
            HighestPointsWord = _internalStateMachine.HighestPointsWord,
            LongestWord = _internalStateMachine.LongestWord,
            PointsNumber = _internalStateMachine.PointsNumber
        };
    }

    public void Reset()
    {
        _internalStateMachine = new();
    }

    public InputResultDto ProcessInputWord(string word)
    {
        word = word.ToUpperInvariant();
        var processedResult = new InputResultDto 
        { 
            InputWordEvaluated = word, 
            WordAccepted = true, 
            WordExists = true 
        };


        char[] invalidLetters = PrevalidateInput(word);
        if (invalidLetters.Length > 0) 
        {
            processedResult.WordAccepted = false;
            processedResult.InvalidLetters = invalidLetters;

            return processedResult;
        }

        if (!_wordsRepository.Exists(word)) 
        {
            processedResult.WordAccepted = false;
            processedResult.WordExists = false;

            return processedResult;
        }

        char[] unavailableLetters = ValidateLetterAvailability(word);
        if (unavailableLetters.Length > 0) 
        {
            processedResult.WordAccepted = false;
            processedResult.NotAvailableLetters = unavailableLetters;

            return processedResult;
        }
        else
        {
            UpdateLetterAvailability(word);
        }

        processedResult.WordPoints = CalculatePoints(word);
        int highestPoints = CalculatePoints(_internalStateMachine.HighestPointsWord);
        if (highestPoints < processedResult.WordPoints)
        {
            _internalStateMachine.HighestPointsWord = processedResult.InputWordEvaluated;
        }
        if (_internalStateMachine.LongestWord.Length < processedResult.InputWordEvaluated.Length)
        {
            _internalStateMachine.LongestWord = processedResult.InputWordEvaluated;
        }
        _internalStateMachine.PointsNumber += processedResult.WordPoints;

        return processedResult;
    }

    private char[] ValidateLetterAvailability(string word) 
    {
        char[] distinctChars = word.ToCharArray();
        List<char> notAvailableChars = [];
        foreach (char letter in distinctChars)
        {
            int count = word.Count(c => c == letter);
            if (LetterAvailability.Instance[letter] < count + _internalStateMachine[letter])
            {
                if (!notAvailableChars.Contains(letter)) 
                {
                    notAvailableChars.Add(letter);
                }
            }
        }

        return notAvailableChars.ToArray(); 
    }

    /// <summary>
    /// Updates the internal state of letter availability
    /// </summary>
    /// <remarks>Performs update only after validates that all letters are available</remarks>
    private void UpdateLetterAvailability(string word)
    {
        char[] distinctChars = word.ToUpperInvariant().ToCharArray();
        List<char> notAvailableChars = new();
        foreach (char letter in distinctChars)
        {
            int count = word.Count(c => c == letter);
            _internalStateMachine[letter] = _internalStateMachine[letter] + count;
        }
    }

    private static char[] PrevalidateInput(string word) 
    {
        //usually this method would be a standalone class, with much more solid validation (not stopping at the first broken rule)
        char[] chars = word.ToCharArray();

        List<char> invalidChars = new();
        foreach (char c in chars) 
        {
            if (!LetterAvailability.Instance.CharacterExists(c))
            {
                if (!invalidChars.Contains(c))
                {
                    invalidChars.Add(c);
                }
            }
        }

        return invalidChars.ToArray();
    }

    private static int CalculatePoints(string word)
    {
        if (string.IsNullOrEmpty(word))
        {
            return 0; 
        }

        char[] chars = word.ToCharArray();
        int overallPoints = 0;
        foreach (char c in chars)
        {
            overallPoints += LetterPoints.Instance[c];
        }

        return overallPoints;
    }

    private class InternalStateMachine
    {
        public int PointsNumber { get; set; }
        public string LongestWord { get; set; } = string.Empty;
        public string HighestPointsWord { get; set; } = string.Empty;

        public int this[char key]
        {
            get => _lettersUsedDictionary[key];
            set
            {
                if (!_lettersUsedDictionary.ContainsKey(key))
                {
                    throw new KeyNotFoundException($"Key '{key}' not found in the dictionary.");
                }

                _lettersUsedDictionary[key] = value;
            }
        }

        private readonly Dictionary<char, int> _lettersUsedDictionary = new()
        {
            ['E'] = 0,
            ['A'] = 0,
            ['I'] = 0,
            ['O'] = 0,
            ['N'] = 0,
            ['R'] = 0,
            ['T'] = 0,
            ['D'] = 0,
            ['L'] = 0,
            ['S'] = 0,
            ['U'] = 0,
            ['G'] = 0,
            ['B'] = 0,
            ['C'] = 0,
            ['F'] = 0,
            ['H'] = 0,
            ['M'] = 0,
            ['P'] = 0,
            ['V'] = 0,
            ['W'] = 0,
            ['Y'] = 0,
            ['J'] = 0,
            ['K'] = 0,
            ['Q'] = 0,
            ['X'] = 0,
            ['Z'] = 0
        };
    }
}
