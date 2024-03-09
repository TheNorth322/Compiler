using System;
using System.Collections.Generic;
using Compiler.data.lexer.Interface;
using Compiler.data.parser.Interface;
using Compiler.domain.entity;
using Compiler.domain.enums;
using Compiler.domain.repository;

namespace Compiler.data.service;

public class CompilerService : ICompilerRepository
{
    private ILexer _lexer;
    private IParser _parser;

    public CompilerService(ILexer lexer, IParser parser)
    {
        _lexer = lexer;
        _parser = parser;
    }
    public List<Lexeme> Analyze(string input)
    {
        return _lexer.Analyze(input);
    }

    public List<ErrorLexeme> Parse(string input)
    {
        return _parser.Parse(input);
    } 
}