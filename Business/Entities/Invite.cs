namespace Business.Entities;

public class Invite
{
    public int Id { get; set; }

    public string Key { get; set; } = string.Empty;

    public int ChatId { get; set; }

    public int UserId { get; set; }
}   