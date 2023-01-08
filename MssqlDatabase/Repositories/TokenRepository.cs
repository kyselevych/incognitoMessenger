using Business.Entities;
using Business.Repositories;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MssqlDatabase.Repositories;

public class TokenRepository : ITokenRepository
{
    private readonly string? stringConnection;

    public TokenRepository(IConfiguration configuration)
    {   
        stringConnection = configuration.GetConnectionString("MssqlDb");

        if (stringConnection == null)
            throw new ArgumentNullException("MssqlDb string connection is null!");
    }

    public RefreshToken? GetByUserId(int userId)
    {
        var query = @"
            SELECT Id, Token, UserId, ExpiryTime
            FROM RefreshTokens
            WHERE UserId = @UserId
        ";

        using var connection = new SqlConnection(stringConnection);

        return connection.QuerySingleOrDefault<RefreshToken>(query, new {UserId = userId});
    }

    public void Insert(RefreshToken refreshTokenModel)
    {
        var query = @"
            INSERT INTO RefreshTokens (UserId, Token, ExpiryTime)
            VALUES (@UserId, @Token, @ExpiryTime)
        ";

        using var connection = new SqlConnection(stringConnection);

        connection.Execute(query, refreshTokenModel);
    }
    
    public void Update(RefreshToken refreshTokenModel)
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