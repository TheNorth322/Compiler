using Compiler.domain.repository;

namespace Compiler.domain.useCases;

public class FileUseCase
{
    private IFileRepository _fileRepository;

    public FileUseCase(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }
}