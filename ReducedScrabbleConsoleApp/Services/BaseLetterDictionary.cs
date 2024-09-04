namespace ReducedScrabbleConsoleApp.Services;

internal abstract class BaseLetterDictionary
{
    protected Dictionary<char, int> _letterDictionary;

    protected BaseLetterDictionary() => InitializeDictionary();

    protected abstract void InitializeDictionary();

    public virtual int this[char key]
    {
        get =>
            _letterDictionary.TryGetValue(key, out int value)
                ? value
                : throw new KeyNotFoundException($"Key {key} not found!");

        set => throw new NotImplementedException();
    }
}
