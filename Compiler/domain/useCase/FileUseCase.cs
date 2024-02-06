using Compiler.domain.entity;
using Compiler.domain.repository;

namespace Compiler.domain.useCases;

public class FileUseCase
{
    private IFileRepository _fileRepository;

    public FileUseCase(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public void CreateFile()
    {
        
    }

    public FileInfo OpenFile(string filePath)
    {
        return _fileRepository.OpenFile(filePath);
    }

    public void SaveFile()
    {
        
    }

    public void SaveFileAs()
    {
        
    }
}