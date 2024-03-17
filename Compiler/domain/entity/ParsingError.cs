using System.Collections.Generic;
using Compiler.data.parser;
using Compiler.domain.enums;
using Compiler.utils;

namespace Compiler.domain.entity;

public class ParsingError
{
    public ParsingError(string expectedLexeme, List<ErrorFragment> errors, ParsingErrorType type)
    {
        ExpectedLexeme = expectedLexeme;
        Errors = errors;
        ParsingErrorType = type;
    }

    public string ExpectedLexeme { get; set; }
    public ParsingErrorType ParsingErrorType { get; set; }
    public List<ErrorFragment> Errors { get; set; }
}