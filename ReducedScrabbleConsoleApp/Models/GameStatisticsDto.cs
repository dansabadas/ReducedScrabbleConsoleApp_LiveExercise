namespace ReducedScrabbleConsoleApp.Models;

public class GameStatisticsDto
{
    public int PointsNumber { get; set; }
    public string LongestWord { get; set; } = string.Empty;
    public string HighestPointsWord { get; set; } = string.Empty;
}
