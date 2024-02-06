using Compiler.domain.entity;

namespace Compiler.data.interfaces;

public interface IFileService
{
    void CreateFile();
    FileInfo OpenFile(string filePath);
    void SaveFile();
    void SaveFileAs();
}