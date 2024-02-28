namespace Compiler.domain.repository;

public interface IReferenceRepository
{
    void CallReference(string localization);
    void CallProgramDescription(string localization);
}