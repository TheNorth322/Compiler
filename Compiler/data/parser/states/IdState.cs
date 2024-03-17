using Compiler.data.parser.Interface;
using Compiler.domain.entity;
using Compiler.domain.enums;
using Compiler.utils;

namespace Compiler.data.parser.states;

public class IdState : IState
{
    private IParser _parser;
    public ParsingError? ErrorLexeme { get; set; }

    private const string ExpectedValue = "identifier";
    public IdState(IParser parser)
    {
        _parser = parser;
    }

    public bool Parse(Lexeme lexeme)
    {
        if (lexeme.Type != LexemeType.Identifier)
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
            ErrorLexeme = new ParsingError("ID", lexeme.Text, lexeme.StartIndex, lexeme.EndIndex, lexeme.Text, false);
        else
            UpdateError(lexeme); 
    }

    private void UpdateError(Lexeme lexeme)
    {
        ErrorLexeme.ReceivedLexeme += lexeme.Text;
        ErrorLexeme.EndIndex = lexeme.EndIndex;
        ErrorLexeme.PartToDismiss += lexeme.Text;
    }
}