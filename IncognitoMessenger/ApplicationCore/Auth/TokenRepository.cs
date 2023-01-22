using Business.Entities;

namespace MssqlDatabase.Repositories;

public class TokenRepository
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

    public RefreshToken? GetByToken(string value)
    {
        return context.RefreshTokens.Where(token => token.Token == value).FirstOrDefault();
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
        var refreshToken = context.RefreshTokens.SingleOrDefault(x => x.UserId == userId);

        if (refreshToken != null)
        {
            context.RefreshTokens.Remove(refreshToken);
            context.SaveChanges();
        }
    }

    public void DeleteByToken(string token)
    {
        var refreshToken = context.RefreshTokens.SingleOrDefault(x => x.Token == token);

        if (refreshToken != null)
        {
            context.RefreshTokens.Remove(refreshToken);
            context.SaveChanges();
        }
    }
}