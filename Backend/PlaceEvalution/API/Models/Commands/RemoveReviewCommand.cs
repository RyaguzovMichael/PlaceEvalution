namespace PlaceEvolution.API.API.Models.Commands;

public class RemoveReviewCommand
{
    public int PlaceId { get; set; }
    public int ReviewId { get; set; }
}
