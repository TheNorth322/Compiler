using Compiler.data.parser.Interface;
using Compiler.domain.entity;
using Compiler.domain.enums;
using Compiler.utils;

namespace Compiler.data.parser.states;

public class TypeAssignState : IState
{
    private IParser _parser;
    public ParsingError? ErrorLexeme { get; set; }
    private const string ExpectedLexeme = ":";

    public TypeAssignState(IParser parser)
    {
        _parser = parser;
    }

    public bool Parse(Lexeme lexeme)
    {
        if (lexeme.Type != LexemeType.TypeAssignmentOperator)
        {
            // Try to find expected lexeme in received
            ParsingError errorLexeme = _parser.FindLexeme(lexeme, LexemeType.Const, ExpectedLexeme);

            RememberLexeme(errorLexeme);
            CheckExpectedInReceived(errorLexeme);

            return false;
        }
        else
        {
            if (ErrorLexeme != null)
            {
                AddLexeme();
                return true;
            }

            _parser.MoveState();
            return true;
        }
    }

    private void CheckExpectedInReceived(ParsingError parsingError)
    {
        // If expected value found in received
        if (parsingError.IsExpectedInReceived)
            AddLexeme();
    }

    private void AddLexeme()
    {
        _parser.AddErrorLexeme(ErrorLexeme);
        ErrorLexeme = null;
        _parser.MoveState();
    }

    private void RememberLexeme(ParsingError errorLexeme)
    {
        if (ErrorLexeme == null)
            ErrorLexeme = errorLexeme;
        else
        {
            UpdateError(errorLexeme); 
        }
    }

    private void UpdateError(ParsingError lexeme)
    {
        ErrorLexeme.ReceivedLexeme += lexeme.ReceivedLexeme;
        ErrorLexeme.EndIndex = lexeme.EndIndex;
        ErrorLexeme.PartToDismiss += lexeme.PartToDismiss;
    }
}