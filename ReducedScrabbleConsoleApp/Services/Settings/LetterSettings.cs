namespace ReducedScrabbleConsoleApp.Services.Settings;

internal class LetterSettings
{
    private static readonly LetterSettings _instance = new();
    public static LetterSettings Instance => _instance;

    private LetterSettings() 
    { 
        LetterPoints = new LetterPoints();
        LetterAvailability = new LetterAvailability();
    }

    public LetterPoints LetterPoints { get; }
    public LetterAvailability LetterAvailability { get; }
}
