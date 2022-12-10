namespace LastExamBackEndProject.API.Contracts;

public interface IFileDbService
{
    Task DeleteFileAsync(string photoLink, CancellationToken cancellationToken);
    Task<string> SaveFileAsync(IFormFile file, CancellationToken cancellationToken);
}