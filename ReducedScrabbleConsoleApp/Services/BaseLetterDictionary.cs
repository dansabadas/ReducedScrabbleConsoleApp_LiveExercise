namespace ReducedScrabbleConsoleApp.Services;

internal abstract class BaseLetterDictionary
{
    protected Dictionary<char, int> _letterDictionary;

    public int this[char key] =>
        _letterDictionary.TryGetValue(key, out int value)
                ? value
                : throw new KeyNotFoundException($"Key {key} not found!");
}
