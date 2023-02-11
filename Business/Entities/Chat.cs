namespace Business.Entities;

public class Chat
{
    public Chat()
    {
        Users = new HashSet<User>();
        Messages = new HashSet<Message>();
    }

    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public int UserId { get; set; }

    public virtual ICollection<Message> Messages { get; set; }
    public virtual ICollection<User> Users { get; set; }
}   