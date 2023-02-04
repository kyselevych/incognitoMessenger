namespace Business.Entities;

public class Member
{
    public int Id { get; set; }

    public int ChatId { get; set; }

    public Chat Chat { get; set; } = null!;

    public int UserId { get; set; }
}   