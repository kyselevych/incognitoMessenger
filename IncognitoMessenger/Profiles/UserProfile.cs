using AutoMapper;
using Business.Entities;
using IncognitoMessenger.Models.User;

namespace IncognitoMessenger.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserRegisterModel, UserModel>();
        CreateMap<UserModel, UserAuthResponseModel>();
    }
}