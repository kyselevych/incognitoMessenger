using Business.Entities;

namespace Business.Repositories;

public interface IUserRepository
{
    IEnumerable<UserModel> GetAll();
    
    UserModel? GetById(int userId);  
    
    UserModel? GetByLogin(string userLogin);  
        
    void Insert(UserModel userModel);
        
    void Delete(int userId);
    
    void Update(UserModel userModel);
}