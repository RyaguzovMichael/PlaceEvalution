﻿using LastExamBackEndProject.API.Contracts;
using LastExamBackEndProject.Common.Exceptions;

namespace LastExamBackEndProject.Infrastructure.Services;

public class FileDbService : IFileDbService
{
    private readonly string _directoryPath;

    public FileDbService()
    {
        _directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/data");
        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }
    }

    public Task DeleteFileAsync(string photoLink, CancellationToken cancellationToken)
    {
        string path = Path.Combine(_directoryPath, photoLink);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        return Task.CompletedTask;
    }

    public async Task<string> SaveFileAsync(IFormFile file, CancellationToken cancellationToken)
    {
        CheckFileExtensions(file);
        string path = Path.Combine(_directoryPath, file.FileName);
        using var fileStream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(fileStream, cancellationToken);
        return file.FileName;
    }

    private static void CheckFileExtensions(IFormFile file)
    {
        string fileExtension = Path.GetExtension(file.FileName);
        if (fileExtension != ".jpg" && fileExtension != ".png")
        {
            throw new ValidationDataException("File can be only have extension of .jpg or .png");
        }
    }
}
