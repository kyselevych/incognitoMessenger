using Business.Entities;

namespace Business.Repositories;

public interface ITokenRepository
{
    RefreshToken? GetByUserId(int userId);

    void Insert(RefreshToken refreshTokenModel);

    void DeleteByUserId(int userId);

    void Update(RefreshToken refreshTokenModel);
}