using Business.Entities;

namespace Business.Repositories;

public interface ITokenRepository
{
    RefreshTokenModel? GetByUserId(int userId);

    void Insert(RefreshTokenModel refreshTokenModel);

    void DeleteByUserId(int userId);

    void Update(RefreshTokenModel refreshTokenModel);
}