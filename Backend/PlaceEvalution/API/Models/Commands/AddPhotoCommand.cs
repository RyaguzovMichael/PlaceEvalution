namespace PlaceEvalution.API.API.Models.Commands;

public class AddPhotoCommand
{
    public int PlaceId { get; set; }
    public IFormFile Photo { get; set; }
}
