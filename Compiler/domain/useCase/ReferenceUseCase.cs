using Compiler.domain.repository;

namespace Compiler.domain.useCases;

public class ReferenceUseCase
{
    private IReferenceRepository _referenceRepository;

    public ReferenceUseCase(IReferenceRepository referenceRepository)
    {
        _referenceRepository = referenceRepository;
    }

    public void CallReference(string localization)
    {
        _referenceRepository.CallReference(localization); 
    }

    public void CallProgramDescription(string localization)
    {
        _referenceRepository.CallProgramDescription(localization);
    }
}