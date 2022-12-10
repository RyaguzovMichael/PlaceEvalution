namespace LastExamBackEndProject.API.Models.ViewModels;

public class ReviewVm
{
    public int Rate { get; private set; }
    public string ReviewText { get; private set; }
    public CustomerVm Customer { get; private set; }
    public string ReviewDate { get; private set; }
}
