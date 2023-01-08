using Business.Entities;
using Business.Repositories;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MssqlDatabase.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string? stringConnection;

    public UserRepository(IConfiguration configuration)
    {
        stringConnection = configuration.GetConnectionString("MssqlDb");

        if (stringConnection == null)
            throw new ArgumentNullException("MssqlDb string connection is null!");
    }

    public IEnumerable<User> GetAll()
    {
        using var context = new ApplicationContext();

        return connection.Query<User>(query);
    }

    public User GetById(int userId)
    {
        var query = @"
            SELECT Id, Login, Password, Pseudonym
            FROM Users
            WHERE Id = @userId
        ";

        using var connection = new SqlConnection(stringConnection);

        return connection.QuerySingleOrDefault<User>(query, new {userId});
    }
    
    public User? GetByLogin(string userLogin)
    {
        var query = @"
            SELECT Id, Login, Password, Pseudonym
            FROM Users
            WHERE Login = @UserLogin
        ";

        using var connection = new SqlConnection(stringConnection);

        return connection.QuerySingleOrDefault<User>(query, new {UserLogin = userLogin});
    }

    public int Insert(User userModel)
    {
        var query = @"
            INSERT INTO Users (Login, Password, Pseudonym)
            VALUES (@Login, @Password, @Pseudonym)
            SELECT CAST(scope_identity() AS int)
        ";

        using var connection = new SqlConnection(stringConnection);

        return connection.ExecuteScalar<int>(query, userModel);
    }

    public void Delete(int userId)
    {
        var query = @"
            DELETE FROM Users
            WHERE Id = @Id
        ";

        using var connection = new SqlConnection(stringConnection);

        connection.Execute(query, new {Id = userId});
    }

    public void Update(User userModel)
    {
        var query = @"
            UPDATE Users
            SET Login = @Login, Password = @Password, Pseudonym = @Pseudonym
            WHERE Id = @Id
        ";

        using var connection = new SqlConnection(stringConnection);

        connection.Execute(query, userModel);
    }
}