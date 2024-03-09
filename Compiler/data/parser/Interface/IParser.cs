using System.Collections.Generic;
using Compiler.data.lexer.Interface;
using Compiler.data.parser.states;
using Compiler.domain.entity;

namespace Compiler.data.parser.Interface;

public interface IParser
{
    IState ConstState { get; }
    IState IdSpaceState { get; }
    IState IdState { get; }
    IState TypeAssignState { get; }
    IState TypeState { get; }
    IState AssignState { get; }
    IState StringBeginState { get; }
    IState StringState { get; }
    IState StringEndState { get; }
    IState EndExpressionState { get; }
    
    ILexer Lexer { get; }

    void SetState(IState state);

    void AddErrorLexeme(ErrorLexeme lexeme);
    List<ErrorLexeme> Parse(string input);
}