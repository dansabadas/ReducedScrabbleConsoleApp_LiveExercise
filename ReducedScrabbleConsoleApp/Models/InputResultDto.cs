namespace ReducedScrabbleConsoleApp.Models;

public class InputResultDto
{ 
    public bool WordAccepted { get; set; }
    public int WordPoints { get; set; }
    public char[] NotAvailableLetters { get; set; } = [];
    public char[] InvalidLetters { get; set; } = [];
    public bool WordExists { get; set; }
    public string InputWordEvaluated { get; set; }
}
