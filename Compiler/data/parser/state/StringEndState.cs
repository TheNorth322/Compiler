using System.Collections.Generic;
using System.Linq;
using Compiler.domain.entity;
using Compiler.domain.enums;

namespace Compiler.data.parser.state;

public class StringEndState : IState
{
    private string target = "\'";
    public List<ErrorFragment> ErrorFragments;
    private IParser _parser;
    private int _currentLiterIndex;

    public StringEndState(IParser parser)
    {
        _parser = parser;
        ErrorFragments = new List<ErrorFragment>();
    }

    public ParsingError? Parse(ref int i, string input)
    {
        _currentLiterIndex = 0;
        ErrorFragments = new List<ErrorFragment>();

        bool foundLexeme = false;
        bool addNewErrorFragment = true;

        for (; i < input.Length; i++)
        {
            if (input[i] == target[_currentLiterIndex])
            {
                addNewErrorFragment = true;
                _currentLiterIndex++;
            }
            else
            {
                AddErrorFragment(input, ref i, ref addNewErrorFragment);
            }

            foundLexeme = CheckIfLexemeFound();
            if (foundLexeme) break;
        }

        ParsingError? parsingError = (ErrorFragments.Count != 0) ? new ParsingError(target, ErrorFragments, ParsingErrorType.Regular) : null;
        if (foundLexeme) _parser.MoveState();
        return parsingError;
    }

    private void AddErrorFragment(string input, ref int i, ref bool addNewErrorFragment)
    {
        if (addNewErrorFragment)
        {
            addNewErrorFragment = false;
            ErrorFragments.Add(new ErrorFragment(i, i, $"{input[i]}"));
        }
        else
        {
            ErrorFragments.Last().UpdateFragment(input[i], i);
        }
    }

    private bool CheckIfLexemeFound()
    {
        return _currentLiterIndex == target.Length;
    }
}