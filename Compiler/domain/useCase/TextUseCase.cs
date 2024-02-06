using Compiler.domain.repository;

namespace Compiler.domain.useCases;

public class TextUseCase
{
    private ITextRepository _textRepository;

    public TextUseCase(ITextRepository textRepository)
    {
        _textRepository = textRepository;
    }
}