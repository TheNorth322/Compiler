using System.Collections.Generic;
using Compiler.data.lexer.Interface;
using Compiler.data.parser.states;
using Compiler.domain.entity;
using Compiler.domain.enums;

namespace Compiler.data.parser.Interface;

public interface IParser
{
    ILexer Lexer { get; }
    void MoveState();
    void AddErrorLexeme(ParsingError lexeme);
    ParsingError FindLexeme(Lexeme lexeme, LexemeType expectedType, string expectedValue);
    List<ParsingError> Parse(string input);
}