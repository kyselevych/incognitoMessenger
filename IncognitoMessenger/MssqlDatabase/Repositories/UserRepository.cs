using Business.Entities;
using Business.Repositories;

namespace MssqlDatabase.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext context;

    public UserRepository(DatabaseContext context)
    {
        this.context = context;
    }

    public IEnumerable<User> GetAll()
    {
        return context.Users;
    }

    public User? GetById(int userId)
    {
        return context.Users.Where(user => user.Id == userId).FirstOrDefault();
    }
    
    public User? GetByLogin(string userLogin)
    {
        return context.Users.Where(user => user.Login == userLogin).FirstOrDefault();
    }

    public int Insert(User user)
    {
        var newUser = context.Users.Add(user);
        context.SaveChanges();
        return newUser.Entity.Id;
    }

    public void Delete(int userId)
    {
        var user = new User() { Id = userId };
        context.Users.Attach(user);
        context.Users.Remove(user);
        context.SaveChanges();
    }

    public void Update(User user)
    {
        context.Users.Update(user);
        context.SaveChanges();
    }
}