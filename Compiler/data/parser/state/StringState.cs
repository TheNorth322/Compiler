using System.Collections.Generic;
using System.Linq;
using Compiler.domain.entity;
using Compiler.domain.enums;

namespace Compiler.data.parser.state;

public class StringState : IState
{
    private string target = "identifier";
    public List<ErrorFragment> ErrorFragments;
    private IParser _parser;

    public StringState(IParser parser)
    {
        _parser = parser;
        ErrorFragments = new List<ErrorFragment>();
    }

    public ParsingError? Parse(ref int i, string input)
    {
        int startIndex = i;

        ErrorFragments.Clear();
        bool addNewErrorFragment = true;
        bool foundLexeme = false;

        ParseUntilEndOfString(input, ref i, ref foundLexeme, ref addNewErrorFragment);

        // Got to end of string and didn't find ':'
        if (!foundLexeme)
        {
            ErrorFragments.Clear();
            ErrorFragments.Add(new ErrorFragment(startIndex, input.Length - 1, input.Substring(startIndex)));
        }

        ParsingError? parsingError = (ErrorFragments.Count != 0) ? new ParsingError(target, ErrorFragments, ParsingErrorType.Regular) : null;
        if (foundLexeme) _parser.MoveState();
        return parsingError;
    }

    private void ParseUntilEndOfString(string input, ref int i, ref bool foundLexeme, ref bool addNewErrorFragment)
    {
        for (; i < input.Length; i++)
        {
            if (input[i] == '\'')
            {
                foundLexeme = true;
                i--;
                break;
            }
        }
    }
}