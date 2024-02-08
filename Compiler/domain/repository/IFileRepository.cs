using Compiler.domain.entity;

namespace Compiler.domain.repository;

public interface IFileRepository
{
    FileInfo OpenFile(string filePath);
    void SaveFile(FileInfo fileInfo);
}