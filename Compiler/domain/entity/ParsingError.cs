using Compiler.utils;

namespace Compiler.domain.entity;

public class ParsingError
{
    public ParsingError(string expectedLexeme, string receivedLexeme, int startIndex, int endIndex, string partToDismiss, bool isExpectedInReceived)
    {
        ExpectedLexeme = expectedLexeme;
        ReceivedLexeme = receivedLexeme;
        StartIndex = startIndex;
        EndIndex = endIndex;
        PartToDismiss = partToDismiss;
        IsExpectedInReceived = isExpectedInReceived;
    }

    public string ExpectedLexeme { get; set; }

    public string ReceivedLexeme { get; set; }

    public int StartIndex { get; set; }
    
    public int EndIndex { get; set; }
    public string PartToDismiss { get; set; }
    
    public bool IsExpectedInReceived { get; set; } 
}