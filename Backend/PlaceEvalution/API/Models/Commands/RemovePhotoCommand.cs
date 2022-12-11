namespace PlaceEvolution.API.API.Models.Commands;

public class RemovePhotoCommand
{
    public int PlaceId { get; set; }
    public string PhotoLink { get; set; }
}
