using Compiler.domain.entity;

namespace Compiler.data.parser.states;

public interface IState
{
   // receive lexeme and verify order
   void Parse(Lexeme lexeme);
}