using System.Collections.Generic;
using Compiler.domain.entity;

namespace Compiler.data.parser;

public interface IParser
{
    public List<ParsingError> Parse(string input);

    public void MoveState();
    string AutoFix(string input);
}