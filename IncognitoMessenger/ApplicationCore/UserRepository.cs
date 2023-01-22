using Business.Entities;

namespace MssqlDatabase.Repositories;

public class UserRepository
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
        return context.Users.Where(user => user.Id == userId).SingleOrDefault();
    }
    
    public User? GetByLogin(string userLogin)
    {
        return context.Users.Where(user => user.Login == userLogin).SingleOrDefault();
    }

    public User Insert(User user)
    {
        var newUser = context.Users.Add(user);
        context.SaveChanges();
        return newUser.Entity;
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