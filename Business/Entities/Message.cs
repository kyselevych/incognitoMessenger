namespace Business.Entities;

public class Message
{
    public int Id { get; set; }

    public string Text { get; set; } = string.Empty;

    public DateTime DateTime { get; set; }

    public int ChatId { get; set; }

    public int UserId { get; set; }
}   