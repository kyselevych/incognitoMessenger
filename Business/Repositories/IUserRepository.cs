using Business.Entities;

namespace Business.Repositories;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    
    User? GetById(int userId);  
    
    User? GetByLogin(string userLogin);  
        
    int Insert(User userModel);
        
    void Delete(int userId);
    
    void Update(User userModel);
}