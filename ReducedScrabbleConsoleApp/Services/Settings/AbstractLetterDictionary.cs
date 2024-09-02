namespace ReducedScrabbleConsoleApp.Services.Settings;

internal abstract class AbstractLetterDictionary
{
    protected Dictionary<char, int> _letterDictionary;

    public int this[char key] =>
        _letterDictionary.TryGetValue(key, out int value)
                ? value
                : throw new KeyNotFoundException($"Key {key} not found!");
}
