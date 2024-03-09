using System.Collections.Generic;
using Compiler.domain.entity;

namespace Compiler.domain.repository;

public interface ICompilerRepository
{
    List<Lexeme> Analyze(string input);
    List<ErrorLexeme> Parse(string input);
}