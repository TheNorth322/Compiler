using Compiler.domain.repository;

namespace Compiler.domain.useCases;

public class EditUseCase
{
    private IEditRepository _editRepository;

    public EditUseCase(IEditRepository editRepository)
    {
        _editRepository = editRepository;
    }
}