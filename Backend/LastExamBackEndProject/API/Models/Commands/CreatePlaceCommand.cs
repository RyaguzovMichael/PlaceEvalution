namespace LastExamBackEndProject.API.Models.Commands;

public class CreatePlaceCommand
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile File { get; set; }
}
