using Compiler.domain.enums;

namespace Compiler.domain.entity;

public struct Lexeme
{
    public LexemeType Type;
    public string Text;
    public int StartIndex;
    public int EndIndex;
    public int LineNumber;

    public Lexeme(LexemeType type, string text, int startIndex, int endIndex, int lineNumber)
    {
        Type = type;
        Text = text;
        StartIndex = startIndex;
        EndIndex = endIndex;
        LineNumber = lineNumber;
    }
}