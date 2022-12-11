namespace PlaceEvolution.API.API.Models.ViewModels;

public class PlaceVm
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string TitlePhotoLink { get; set; }
    public float Rate { get; set; }
    public List<string>? Photos { get; set; }
    public List<ReviewVm>? Reviews { get; set; }
}
