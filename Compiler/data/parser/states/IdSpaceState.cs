using Compiler.data.parser.Interface;
using Compiler.domain.entity;
using Compiler.domain.enums;
using Compiler.utils;

namespace Compiler.data.parser.states;

public class IdSpaceState : IState
{
    private IParser _parser;
    public ParsingError? ErrorLexeme { get; set; }
    private const string ExpectedLexeme = " ";

    public IdSpaceState(IParser parser)
    {
        _parser = parser;
    }

    public bool Parse(Lexeme lexeme)
    {
        if (lexeme.Type != LexemeType.Separator)
        {
           RememberLexeme(lexeme);
           return false;
        }
        else
        {
            if (ErrorLexeme != null)
            {
                _parser.AddErrorLexeme(ErrorLexeme);
                ErrorLexeme = null;
            }

            _parser.MoveState();
            return true;
        }
    }

    private void RememberLexeme(Lexeme lexeme)
    {
        if (ErrorLexeme == null)
            ErrorLexeme = new ParsingError(ExpectedLexeme, lexeme.Text, lexeme.StartIndex, lexeme.EndIndex,
                lexeme.Text);
        else
        {
            ErrorLexeme.ReceivedLexeme += lexeme.Text;
            ErrorLexeme.EndIndex = lexeme.EndIndex;
            ErrorLexeme.PartToDismiss += lexeme.Text;
        }
    }
}