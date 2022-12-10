using LastExamBackEndProject.API.Contracts;
using LastExamBackEndProject.Common.Exceptions;

namespace LastExamBackEndProject.Infrastructure.Services;

public class FileDbService : IFileDbService
{

    public Task DeleteFileAsync(string photoLink, CancellationToken cancellationToken)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", photoLink);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        return Task.CompletedTask;
    }

    public async Task<string> SaveFileAsync(IFormFile file, CancellationToken cancellationToken)
    {
        string fileExtension = Path.GetExtension(file.FileName);
        if ( fileExtension != ".jpg" || fileExtension != ".jpg")
        {
            throw new ValidationDataException("File can be only have extension of .jpg or .png");
        }
        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FileName);
        using var fileStream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(fileStream, cancellationToken);
        return file.FileName;
    }
}
