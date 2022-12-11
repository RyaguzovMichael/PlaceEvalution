namespace PlaceEvalution.API.API.Contracts;

public interface IFileDbService
{
    Task DeleteFileAsync(string photoLink, CancellationToken token);
    Task<string> SaveFileAsync(IFormFile file, CancellationToken token);
}