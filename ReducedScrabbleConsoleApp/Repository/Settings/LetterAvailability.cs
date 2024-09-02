using ReducedScrabbleConsoleApp.Services;

namespace ReducedScrabbleConsoleApp.Repository.Settings;

internal class LetterAvailability
{
    private static readonly LetterAvailability _instance = new();
    public static LetterAvailability Instance => _instance;

    private static readonly Dictionary<char, int> _letterAvailabilityDictionary = new()
    {
        ['E'] = 12,
        ['A'] = 9,        
        ['I'] = 9,
        ['O'] = 8,
        ['N'] = 6,
        ['R'] = 6,
        ['T'] = 6,
        ['D'] = 4,
        ['L'] = 4,        
        ['S'] = 4,
        ['U'] = 4,
        ['G'] = 3,
        ['B'] = 2,
        ['C'] = 2,
        ['F'] = 2,
        ['H'] = 2,
        ['M'] = 2,
        ['P'] = 2,
        ['V'] = 2,
        ['W'] = 2,
        ['Y'] = 2,
        ['J'] = 1,
        ['K'] = 1,
        ['Q'] = 1,
        ['X'] = 1,
        ['Z'] = 1
    };

    public int this[char key] =>
            _letterAvailabilityDictionary.TryGetValue(key, out int value)
                    ? value
                    : throw new KeyNotFoundException($"Key {key} not found!");

    public bool CharacterExists(char character)
    {
        return _letterAvailabilityDictionary.TryGetValue(character, out int _);
    }
}
