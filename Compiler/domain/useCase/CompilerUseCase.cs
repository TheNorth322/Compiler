using System.Collections.Generic;
using Compiler.domain.entity;
using Compiler.domain.repository;

namespace Compiler.domain.useCases;

public class CompilerUseCase
{
    private ICompilerRepository _compilerRepository;

    public CompilerUseCase(ICompilerRepository compilerRepository)
    {
        _compilerRepository = compilerRepository;
    }

    public List<Lexeme> Analyze(string input)
    {
        return _compilerRepository.Analyze(input);
    }

    public List<ParsingError> Parse(string input)
    {
        return _compilerRepository.Parse(input);
    }
}