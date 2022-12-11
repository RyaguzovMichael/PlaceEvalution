namespace PlaceEvolution.API.API.Models.Commands;

public class AddReviewCommand
{
    public int PlaceId { get; set; }
    public int Score { get; set; }
    public string ReviewText { get; set; }
}
