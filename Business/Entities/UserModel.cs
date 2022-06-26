namespace Business.Entities;

public class UserModel
{
    public int Id { get; set; }

    public string Login { get; set; }
    
    public string Password { get; set; }
    
    public string Pseudonym { get; set; }
}