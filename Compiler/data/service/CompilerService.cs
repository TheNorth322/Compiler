using System;
using System.Collections.Generic;
using Compiler.domain.entity;
using Compiler.domain.enums;
using Compiler.domain.repository;

namespace Compiler.data.service;

public class CompilerService : ICompilerRepository
{
    public List<Lexeme> Analyze(string input)
    {
        List<Lexeme> lexemes = new List<Lexeme>();

        int currentIndex = 0;
        int currentLineNumber = 1;
        string lexeme = "";
        
        // Iterate through input string
        while (currentIndex < input.Length)
        {
            lexeme = $"{input[currentIndex]}";

            // Keywords
            if (char.IsLetter(input[currentIndex]))
            {
                int startIndex = currentIndex;

                // Build lexeme until special symbol or separator
                while ((char.IsLetterOrDigit(input[currentIndex + 1]) || input[currentIndex + 1] == '_') &&
                       (currentIndex + 1) < input.Length)
                {
                    currentIndex++;
                    lexeme += input[currentIndex];
                }

                LexemeType type = GetKeywordType(lexeme);
                lexemes.Add(new Lexeme(type, lexeme, startIndex, currentIndex++, currentLineNumber));
            }
            // Constant strings
            else if (input[currentIndex] == '\'')
            {
                int startIndex = currentIndex;

                // Build lexeme until special symbol or separator
                while ((char.IsLetterOrDigit(input[currentIndex + 1]) || input[currentIndex + 1] == '\'') &&
                       (currentIndex + 1) < input.Length)
                {
                    currentIndex++;
                    lexeme += input[currentIndex];
                }

                lexemes.Add(new Lexeme(LexemeType.StringConstant, lexeme, startIndex, currentIndex++, currentLineNumber));
            }
            // Unsigned integers 
            else if (char.IsDigit(input[currentIndex]))
            {
                int startIndex = currentIndex;

                // Build lexeme until special symbol or separator
                while ((char.IsDigit(input[currentIndex + 1])) &&
                       (currentIndex + 1) < input.Length)
                {
                    currentIndex++;
                    lexeme += input[currentIndex];
                }

                lexemes.Add(new Lexeme(LexemeType.UnsignedInteger, lexeme, startIndex, currentIndex++, currentLineNumber));
            }
            else if (input[currentIndex] == '\r')
            {
                currentIndex++;
            }
            else if (input[currentIndex] == '\n')
            {
                currentIndex++;
                currentLineNumber++;
            }
            // Special symbols (:, =, ;, whitespace, \t)
            else
            {
                LexemeType symbolType = GetSymbolType(input[currentIndex]);
                lexemes.Add(new Lexeme(symbolType, lexeme, currentIndex, currentIndex, currentLineNumber));
                currentIndex++;
            }
        }

        return lexemes;
    }

    private LexemeType GetSymbolType(char symbol)
    {
        switch (symbol)
        {
            case ':':
                return LexemeType.TypeAssignmentOperator;
            case '=':
                return LexemeType.AssignmentOperator;
            case ';':
                return LexemeType.EndOfStatement;
            case ' ':
            case '\t':
                return LexemeType.Separator;        
            default:
                return LexemeType.Invalid;
        }
    }

    private LexemeType GetKeywordType(string lexeme)
    {
        switch (lexeme)
        {
            case "Const":
                return LexemeType.Const;
            case "string":
                return LexemeType.String;
            case "integer":
                return LexemeType.Integer;
            case "boolean":
                return LexemeType.Boolean;
            case "true":
                return LexemeType.True;
            case "false":
                return LexemeType.False;
            default:
                return LexemeType.Identifier;
        }
    }
}