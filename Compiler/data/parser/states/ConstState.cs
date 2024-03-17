using Compiler.data.parser.Interface;
using Compiler.domain.entity;
using Compiler.domain.enums;
using Compiler.utils;

namespace Compiler.data.parser.states;

public class ConstState : IState
{
    private IParser _parser;

    public ParsingError? ErrorLexeme { get; set; }

    private const string ExpectedLexeme = "Const";

    public ConstState(IParser parser)
    {
        _parser = parser;
    }

    public bool Parse(Lexeme lexeme)
    {
        if (lexeme.Type != LexemeType.Const)
        {
            // Try to find expected lexeme in received (CConstt -> C Const t)
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
        // If expected value found in received. Add parsing error and move state
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
        // if previous and this one do not have expected value inside
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