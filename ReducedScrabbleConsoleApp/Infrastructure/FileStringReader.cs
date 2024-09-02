namespace ReducedScrabbleConsoleApp.Infrastructure;

using System.Reflection;

public class FileStringReader
{
    public static string[] ReadFile()
    {
        List<string> lines = [];

        Stream stream = Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream("ReducedScrabbleConsoleApp.Data.WordGameExercise_Dictionary_SupportFile.txt");
        using (StreamReader sr = new StreamReader(stream))
        {
            string currentLine;
            while ((currentLine = sr.ReadLine()) != null)
            {
                lines.Add(currentLine);
            }
        }

        return lines.ToArray();
    }
}
