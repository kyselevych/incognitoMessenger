using AutoMapper;
using Business.Entities;
using IncognitoMessenger.ApplicationCore.Chats.Models;
using IncognitoMessenger.Models.User;

namespace IncognitoMessenger.Profiles;

public class ChatProfile : Profile
{
    public ChatProfile()
    {
        CreateMap<CreateChatModel, Chat>();
    }
}