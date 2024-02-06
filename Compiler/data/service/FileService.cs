using System;
using System.IO;
using Compiler.domain.repository;
using FileInfo = Compiler.domain.entity.FileInfo;

namespace Compiler.data.service;

public class FileService : IFileRepository
{
    public void CreateFile()
    {
        
    }

    public FileInfo OpenFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new ArgumentException("File not exists");

        using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
        byte[] buffer = new byte[fileStream.Length];
        int bytesRead = fileStream.Read(buffer, 0, buffer.Length);
        string fileContent = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);

        return new FileInfo(GetFileName(filePath), filePath, GetFileExtension(filePath), fileContent);
    }

    public void SaveFile()
    {
        throw new System.NotImplementedException();
    }

    public void SaveFileAs()
    {
        throw new System.NotImplementedException();
    }
    
    private string GetFileName(string filePath)
    {
        return Path.GetFileName(filePath);
    }

    private string GetFileExtension(string filePath)
    {
        return Path.GetExtension(filePath);
    }
}