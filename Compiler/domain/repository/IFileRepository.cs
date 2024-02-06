namespace Compiler.domain.repository;

public interface IFileRepository
{
    void CreateFile();
    void OpenFile();
    void SaveFile();
    void SaveFileAs();
    void Exit();
}