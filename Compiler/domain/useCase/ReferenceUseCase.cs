using Compiler.domain.repository;

namespace Compiler.domain.useCases;

public class ReferenceUseCase
{
    private IReferenceRepository _referenceRepository;

    public ReferenceUseCase(IReferenceRepository referenceRepository)
    {
        _referenceRepository = referenceRepository;
    }
}