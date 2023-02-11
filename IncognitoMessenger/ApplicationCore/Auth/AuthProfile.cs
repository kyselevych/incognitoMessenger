using AutoMapper;
using Business.Entities;
using IncognitoMessenger.Models.User;

namespace IncognitoMessenger.Profiles;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<UserRegisterCredential, User>();
        CreateMap<UserLoginCredential, User>();
        CreateMap<User, UserSecure>();
    }
}