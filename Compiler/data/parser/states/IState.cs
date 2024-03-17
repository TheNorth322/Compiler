using Compiler.domain.entity;

namespace Compiler.data.parser.states;

public interface IState
{
    ParsingError? ErrorLexeme { get; set; }

    // receive lexeme and verify order
    bool Parse(Lexeme lexeme);
}