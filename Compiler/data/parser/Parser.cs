using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.data.lexer.Interface;
using Compiler.data.parser.Interface;
using Compiler.data.parser.states;
using Compiler.domain.entity;
using Compiler.domain.enums;
using Compiler.utils;

namespace Compiler.data.parser;

public class Parser : IParser
{
    public ILexer Lexer { get; }
    private List<ParsingError> _errorLexemes;
    private List<IState> _states;
    private int _currentState;
    
    public Parser(ILexer lexer)
    {
        this.Lexer = lexer;
        _errorLexemes = new List<ParsingError>();
        _states = new List<IState>();

        _states.Add(new ConstState(this));
        _states.Add(new IdSpaceState(this));
        _states.Add(new IdState(this));
        _states.Add(new TypeAssignState(this));
        _states.Add(new TypeState(this));
        _states.Add(new AssignState(this));
        _states.Add(new StringBeginState(this));
        _states.Add(new StringState(this));
        _states.Add(new StringEndState(this));
        _states.Add(new EndExpressionState(this));
    }

    public void MoveState()
    {
        _currentState = (_states.Count - 1 == _currentState)
            ? 0
            : _currentState + 1;
    }

    public void AddErrorLexeme(ParsingError lexeme)
    {
        _errorLexemes.Add(lexeme);
    }

    public ParsingError FindLexeme(Lexeme lexeme, LexemeType expectedType, string expectedValue)
    {
        int index = lexeme.Text.IndexOf(expectedValue, StringComparison.Ordinal);
        if (index != -1)
        {
            int start = index;
            int end = start + expectedValue.Length;

            string leftPart = lexeme.Text.Substring(0, start);
            string rightPart = lexeme.Text.Substring(end);

            return new ParsingError(expectedValue, lexeme.Text, lexeme.StartIndex, lexeme.StartIndex + end,
                $"{leftPart} {rightPart}", true);
        }
        else
        {
            return new ParsingError(expectedValue, lexeme.Text, lexeme.StartIndex, lexeme.EndIndex,
                $"{lexeme.Text}", false);
        }
    }


    public List<ParsingError> Parse(string input)
    {
        List<Lexeme> lexemes = Lexer.Analyze(input);

        _errorLexemes.Clear();
        _currentState = 0;

        // Iterate through lexemes
        for (int i = 0; i < lexemes.Count; i++)
        {
            Lexeme lexeme = lexemes[i];
            
            // Try to parse lexeme
            bool lexemeFound = _states[_currentState].Parse(lexeme);
            
            // Check if there is an error after parsing this lexeme
            if (!lexemeFound)
            {
                IronsMethod(lexemes, ref i);
            }
        }

        CheckError();
        return _errorLexemes;
    }

    private void IronsMethod(List<Lexeme> lexemes, ref int currentIndex)
    {
        int nextState = _currentState;
              
        while (_states[nextState].ErrorLexeme != null && currentIndex < lexemes.Count - 1)
        {
            // Move to the next lexeme
            currentIndex++;
            Lexeme nextLexeme = lexemes[currentIndex];

            // Try to parse the lexeme with the current state
            bool result = _states[nextState].Parse(nextLexeme);

            // If no error found, update the state and exit the loop
            if (result)
                break;
        }
    }

    private void CheckError()
    {
        if (_states[_currentState].ErrorLexeme != null)
        {
            this.AddErrorLexeme(_states[_currentState].ErrorLexeme);
            _states[_currentState].ErrorLexeme = null;
        }
    }
}