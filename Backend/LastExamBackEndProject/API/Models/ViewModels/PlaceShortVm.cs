namespace LastExamBackEndProject.API.Models.ViewModels;

public class PlaceShortVm
{
    public int Id { get; set; }
    public string Title { get; private set; }
    public string TitlePhotoLink { get; private set; }
    public float Rate { get; private set; }
    public int ReviewsCount { get; set; }
    public int PhotosCount { get; set; }
}
