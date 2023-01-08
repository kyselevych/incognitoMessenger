namespace Business.Entities;

public class User
{
    public int Id { get; set; }

    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Nickname { get; set; } = string.Empty;

    public ICollection<Chat>? Chats { get; set; }
}   