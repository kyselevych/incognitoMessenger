using Business.Entities;
using FluentValidation;
using IncognitoMessenger.ApplicationCore.Chats;
using MssqlDatabase.Repositories;

namespace IncognitoMessenger.Validation.Validators;

public class OwnerValidator : AbstractValidator<Member>
{
    public OwnerValidator(ChatRepository chatRepository)
    {
        RuleFor(member => member)
            .Must(member => 
                chatRepository.GetChats(member.UserId)
                    .SingleOrDefault(chat => chat.Id == member.ChatId && chat.UserId == member.UserId) != null
            )
            .WithMessage("You are not owner of this chat");
    }
}