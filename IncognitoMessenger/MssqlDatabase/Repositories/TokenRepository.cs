using Business.Entities;
using Business.Repositories;

namespace MssqlDatabase.Repositories;

public class TokenRepository : ITokenRepository
{
    private readonly DatabaseContext context;

    public TokenRepository(DatabaseContext context)
    {
        this.context = context;
    }

    public RefreshToken? GetByUserId(int userId)
    {
        return context.RefreshTokens.Where(token => token.UserId == userId).FirstOrDefault();
    }

    public void Insert(RefreshToken refreshToken)
    {
        context.RefreshTokens.Add(refreshToken);
        context.SaveChanges();
    }
    
    public void Update(RefreshToken refreshToken)
    {
        context.RefreshTokens.Update(refreshToken);
        context.SaveChanges();
    }
    
    public void DeleteByUserId(int userId)
    {
        var refreshToken = new RefreshToken() { UserId = userId };
        context.RefreshTokens.Attach(refreshToken);
        context.RefreshTokens.Remove(refreshToken);
        context.SaveChanges();
    }
}