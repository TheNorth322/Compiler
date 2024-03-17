using Compiler.domain.entity;

namespace Compiler.data.parser.state;

public interface IState
{
    ParsingError? Parse(ref int startIndex, string input);
}