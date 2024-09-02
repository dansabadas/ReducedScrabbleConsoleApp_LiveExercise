namespace ReducedScrabbleConsoleApp.Repository;

using ReducedScrabbleConsoleApp.Infrastructure;

public class WordsRepository : IWordsRepository
{
    private readonly string[] _words;

    public WordsRepository()
    {
        _words = FileStringReader.ReadFile();
    }

    public bool Exists(string word)
    {
        return _words.Contains(word);
    }
}
