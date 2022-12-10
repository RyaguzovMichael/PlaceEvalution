namespace LastExamBackEndProject.API.Models.ViewModels;

public class PlaceVm
{
    public int Id { get; set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string TitlePhotoLink { get; private set; }
    public float Rate { get; private set; }
    public List<string>? Photos { get; private set; }
    public List<ReviewVm>? Reviews { get; private set; }
}
