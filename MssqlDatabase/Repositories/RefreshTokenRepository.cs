using Business.Entities;
using Business.Repositories;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MssqlDatabase.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly string? stringConnection;

    public RefreshTokenRepository(IConfiguration configuration)
    {   
        stringConnection = configuration.GetConnectionString("MssqlDb");

        if (stringConnection == null)
            throw new ArgumentNullException("MssqlDb string connection is null!");
    }

    public RefreshTokenModel? GetByUserId(int userId)
    {
        var query = @"
            SELECT Id, Token, UserId, ExpiryTime
            FROM RefreshTokens
            WHERE UserId = @userId
        ";

        using var connection = new SqlConnection(stringConnection);

        return connection.QuerySingleOrDefault<RefreshTokenModel>(query, new {userId});
    }

    public void Insert(RefreshTokenModel refreshTokenModel)
    {
        var query = @"
            INSERT INTO RefreshTokens (UserId, Token, ExpiryTime)
            VALUES (UserId, Token, ExpiryTime)
        ";

        using var connection = new SqlConnection(stringConnection);

        connection.Execute(query, refreshTokenModel);
    }
    
    public void Update(RefreshTokenModel refreshTokenModel)
    {
        var query = @"
            UPDATE RefreshTokens
            SET UserId = @UserId, Token = @Token, ExpiryTime = @ExpiryTime
            WHERE Id = @Id
        ";

        using var connection = new SqlConnection(stringConnection);

        connection.Execute(query, refreshTokenModel);
    }
    
    public void DeleteByUserId(int userId)
    {
        var query = @"
            DELETE FROM RefreshTokens
            WHERE UserId = @userId
        ";

        using var connection = new SqlConnection(stringConnection);

        connection.Execute(query, new {UserId = userId});
    }
}