using Compiler.domain.repository;

namespace Compiler.domain.useCases;

public class CompilerUseCase
{
    private ICompilerRepository _compilerRepository;

    public CompilerUseCase(ICompilerRepository compilerRepository)
    {
        _compilerRepository = compilerRepository;
    }
}