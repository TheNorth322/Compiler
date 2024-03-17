using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.domain.entity;
using Compiler.domain.enums;

namespace Compiler.data.parser.state;

public class IdState : IState
{
    private string target = "identifier";
    public List<ErrorFragment> ErrorFragments;
    private IParser _parser;

    public IdState(IParser parser)
    {
        _parser = parser;
        ErrorFragments = new List<ErrorFragment>();
    }

    public ParsingError? Parse(ref int i, string input)
    {
        int startIndex = i;
        
        ErrorFragments.Clear();
        bool addNewErrorFragment = true;
        bool foundId = false;

        // Check for digits and symbols in beginning (e.g. 223id, 2;-3id)
        CheckIncorrectSymbolsAtBeginning(input, ref i, ref addNewErrorFragment);

        addNewErrorFragment = true;
        
        ParseUntilTypeAssignment(input, ref i, ref foundId, ref addNewErrorFragment);
        
        // Got to end of string and didn't find ':'
        if (!foundId)
        {
            ErrorFragments.Clear();
            ErrorFragments.Add(new ErrorFragment(startIndex, input.Length - 1, input.Substring(startIndex)));
        }
            
        ParsingError? parsingError = (ErrorFragments.Count != 0) ? new ParsingError(target, ErrorFragments, ParsingErrorType.Regular) : null;
        if (foundId) _parser.MoveState();
        return parsingError;
    }

    private void CheckIncorrectSymbolsAtBeginning(string input, ref int i, ref bool addNewErrorFragment)
    {
        while (!char.IsLetter(input[i]) && input[i] != '_')
        {
            AddNewErrorFragment(ref addNewErrorFragment, input, i);
            i++;
        }
    }

    private void ParseUntilTypeAssignment(string input, ref int i, ref bool foundId, ref bool addNewErrorFragment)
    {
        for (; i < input.Length; i++)
        {
            if (input[i] == ':')
            {
                foundId = true;
                i--;
                break;
            }
            else if (!char.IsLetterOrDigit(input[i]) && input[i] != '_')
                AddNewErrorFragment(ref addNewErrorFragment, input, i);
            else
                addNewErrorFragment = true;
        }
    }

    private void AddNewErrorFragment(ref bool addNewErrorFragment, string input, int i)
    {
        if (addNewErrorFragment)
        {
            ErrorFragments.Add(new ErrorFragment(i, i, $"{input[i]}"));
            addNewErrorFragment = false;
        }
        else
        {
            ErrorFragments.Last().UpdateFragment(input[i], i);
        }
    }
}