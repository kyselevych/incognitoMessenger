using Business.Entities;
using FluentValidation;
using IncognitoMessenger.ApplicationCore.Chats;
using MssqlDatabase.Repositories;

namespace IncognitoMessenger.Validation.Validators;

public class MemberValidator : AbstractValidator<Member>
{
    public MemberValidator(ChatRepository chatRepository)
    {
        RuleFor(member => member)
            .Must(member => chatRepository.GetChats(member.UserId).SingleOrDefault(x => x.Id == member.ChatId) != null)
            .WithMessage("You are not member of this chat");
    }
}