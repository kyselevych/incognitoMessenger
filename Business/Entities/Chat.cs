namespace Business.Entities;

public class Chat
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public int OwnerUserId { get; set; }
}   