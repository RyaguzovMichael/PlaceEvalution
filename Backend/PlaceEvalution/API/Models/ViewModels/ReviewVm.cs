namespace PlaceEvalution.API.API.Models.ViewModels;

public class ReviewVm
{
    public int Id { get; set; }
    public int Rate { get; set; }
    public string ReviewText { get; set; }
    public CustomerVm Customer { get; set; }
    public string ReviewDate { get; set; }
}
