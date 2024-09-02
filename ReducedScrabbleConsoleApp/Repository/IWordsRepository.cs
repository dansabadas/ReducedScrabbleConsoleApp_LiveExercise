namespace ReducedScrabbleConsoleApp.Repository;

public interface IWordsRepository
{
    bool Exists(string word);
}
