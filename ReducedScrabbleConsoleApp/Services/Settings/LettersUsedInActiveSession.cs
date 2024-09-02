namespace ReducedScrabbleConsoleApp.Services.Settings;

internal class LettersUsedInActiveSession : AbstractLetterDictionary
{
    public LettersUsedInActiveSession()
    {
        _letterDictionary = new()
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

    public new int this[char key]
    {
        get => base[key];
        set
        {
            if (!_letterDictionary.ContainsKey(key))
            {
                throw new KeyNotFoundException($"Key '{key}' not found in the dictionary.");
            }

            _letterDictionary[key] = value;
        }
    }
}
