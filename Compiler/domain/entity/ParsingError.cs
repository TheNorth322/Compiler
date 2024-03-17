using Compiler.utils;

namespace Compiler.domain.entity;

public class ParsingError
{
    public ParsingError(string expectedLexeme, string receivedLexeme, int startIndex, int endIndex, string partToDismiss)
    {
        ExpectedLexeme = expectedLexeme;
        ReceivedLexeme = receivedLexeme;
        StartIndex = startIndex;
        EndIndex = endIndex;
        PartToDismiss = partToDismiss;
    }

    public string ExpectedLexeme { get; set; }

    public string ReceivedLexeme { get; set; }

    public int StartIndex { get; set; }
    
    public int EndIndex { get; set; }
    public string PartToDismiss { get; set; }
}