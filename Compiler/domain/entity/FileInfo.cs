namespace Compiler.domain.entity;

public class FileInfo
{
    public FileInfo(string fileName, string filePath, string fileContents)
    {
        FileName = fileName;
        FilePath = filePath;
        FileContents = fileContents;
    }

    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string FileContents { get; set; }
}