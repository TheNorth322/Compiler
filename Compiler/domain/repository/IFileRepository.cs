﻿using Compiler.domain.entity;

namespace Compiler.domain.repository;

public interface IFileRepository
{
    void CreateFile();
    FileInfo OpenFile(string filePath);
    void SaveFile();
    void SaveFileAs();
}