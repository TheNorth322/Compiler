namespace Compiler.domain.repository;

public interface IEditRepository
{
    void Cancel();
    void Repeat();
    void Cut();
    void Copy();
    void Put();
    void Delete();
    void SelectAll();
}