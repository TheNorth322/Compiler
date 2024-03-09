using System.Collections.Generic;
using System.Linq;
using Compiler.data.lexer.Interface;
using Compiler.data.parser.Interface;
using Compiler.data.parser.states;
using Compiler.domain.entity;
using Compiler.domain.enums;

namespace Compiler.data.parser;

public class Parser : IParser
{
    public ILexer Lexer { get; }
    private List<ErrorLexeme> _errorLexemes;
    private IState _currentState;

    public IState ConstState { get; }
    public IState IdSpaceState { get; }
    public IState IdState { get; }
    public IState TypeAssignState { get; }
    public IState TypeState { get; }
    public IState AssignState { get; }
    public IState StringBeginState { get; }
    public IState StringState { get; }
    public IState StringEndState { get; }
    public IState EndExpressionState { get; }

    public Parser(ILexer lexer)
    {
        this.Lexer = lexer;
        _errorLexemes = new List<ErrorLexeme>();

        ConstState = new ConstState(this);
        IdSpaceState = new IdSpaceState(this);
        IdState = new IdState(this);
        TypeAssignState = new TypeAssignState(this);
        TypeState = new TypeState(this);
        AssignState = new AssignState(this);
        StringBeginState = new StringBeginState(this);
        StringState = new StringState(this);
        StringEndState = new StringEndState(this);
        EndExpressionState = new EndExpressionState(this);
    }

    public void SetState(IState state)
    {
        _currentState = state;
    }

    public void AddErrorLexeme(ErrorLexeme lexeme)
    {
        _errorLexemes.Add(lexeme);
    }

    public List<ErrorLexeme> Parse(string input)
    {
        List<Lexeme> lexemes = Lexer.Analyze(input);

        _errorLexemes.Clear();
        _currentState = ConstState;


        foreach (Lexeme lexeme in lexemes)
        {
            _currentState.Parse(lexeme);
        }

        Lexeme lastLexeme = lexemes.Last();
        if (lastLexeme.Type != LexemeType.EndOfStatement)
        {
            _errorLexemes.Add(new ErrorLexeme(lastLexeme, "waited for ;"));
        }

        return _errorLexemes;
    }
}