using System.Collections.Generic;
using Compiler.domain.entity;

namespace Compiler.data.lexer.Interface;

public interface ILexer
{
    List<Lexeme> Analyze(string input);
}