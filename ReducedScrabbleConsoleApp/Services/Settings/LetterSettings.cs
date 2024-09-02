namespace ReducedScrabbleConsoleApp.Services.Settings;

internal class LetterSettings
{
    private static readonly LetterSettings _instance = new();
    public static LetterSettings Instance => _instance;

    private LetterSettings() 
    { 
        LetterPoints = LetterPoints.Instance;
        LetterAvailability = LetterAvailability.Instance;
    }

    public LetterPoints LetterPoints { get; private set; }
    public LetterAvailability LetterAvailability { get; private set; }
}
