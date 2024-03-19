using System;
using System.Collections.Generic;
using System.Printing;
using Compiler.data.parser.state;
using Compiler.domain.entity;
using Compiler.domain.enums;

namespace Compiler.data.parser;

public class Parser : IParser
{
    private List<IState> _states;
    private int _currentStateIndex;

    public Parser()
    {
        _states = new List<IState>();

        _states.Add(new ConstState(this));
        _states.Add(new IdSpaceState(this));
        _states.Add(new IdState(this));
        _states.Add(new TypeAssignmentState(this));
        _states.Add(new TypeState(this));
        _states.Add(new AssignmentState(this));
        _states.Add(new StringBeginState(this));
        _states.Add(new StringState(this));
        _states.Add(new StringEndState(this));
        _states.Add(new EndExpressionState(this));
    }

    public List<ParsingError> Parse(string input)
    {
        _currentStateIndex = 0;
        List<ParsingError> parsingErrors = new List<ParsingError>();

        for (int i = 0; i < input.Length - 1; i++)
        {
            ParsingError? parsingError = _states[_currentStateIndex].Parse(ref i, input);
            AddParsingError(parsingErrors, parsingError);
        }

        CheckForUnfinishedString(parsingErrors);
        return parsingErrors;
    }

    public void MoveState()
    {
        _currentStateIndex = (_currentStateIndex == _states.Count - 1) ? 0 : _currentStateIndex + 1;
    }

    public string AutoFix(string input)
    {
        _currentStateIndex = 0;

        for (int i = 0; i < input.Length - 1; i++)
        {
            ParsingError? parsingError = _states[_currentStateIndex].Parse(ref i, input);
            FixParsingError(ref input, ref i, parsingError);
        }

        return input;
    }

    private void FixParsingError(ref string input, ref int i, ParsingError? parsingError)
    {
        if (parsingError != null)
        {
            for (int j = 0; j < parsingError.Errors.Count; j++)
            {
                ErrorFragment fragment = parsingError.Errors[j];
                int length = fragment.EndIndex - fragment.StartIndex + 1;
                
                input = input.Remove(fragment.StartIndex, length);
                i -= length;
                
                FixStartIndex(j, length, parsingError);
            }
        }
    }

    private void FixStartIndex(int fragmentErrorIndex, int length, ParsingError? parsingError)
    {
        fragmentErrorIndex++;
        for (; fragmentErrorIndex < parsingError.Errors.Count; fragmentErrorIndex++)
        {
            parsingError.Errors[fragmentErrorIndex].StartIndex -= length;
            parsingError.Errors[fragmentErrorIndex].EndIndex -= length;
        } 
    }
    
    private void CheckForUnfinishedString(List<ParsingError> parsingErrors)
    {
        if (_currentStateIndex != _states.Count - 1)
        {
            parsingErrors.Add(new ParsingError("unfinished string", new List<ErrorFragment>(),
                ParsingErrorType.UnfinishedString));
        }
    }

    private void AddParsingError(List<ParsingError> parsingErrors, ParsingError? parsingError)
    {
        if (parsingError != null)
            parsingErrors.Add(parsingError);
    }
}