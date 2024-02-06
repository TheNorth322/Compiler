namespace Compiler.domain.entity;

public class FileInfo
{
    public FileInfo(string fileName, string filePath, string fileExtension, string fileContents)
    {
        FileName = fileName;
        FilePath = filePath;
        FileExtension = fileExtension;
        FileContents = fileContents;
    }

    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string FileExtension { get; set; }
    public string FileContents { get; set; }
}