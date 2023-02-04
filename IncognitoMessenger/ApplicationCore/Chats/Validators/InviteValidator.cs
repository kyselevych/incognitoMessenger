using Business.Entities;
using FluentValidation;
using IncognitoMessenger.ApplicationCore.Chats;
using IncognitoMessenger.ApplicationCore.Chats.Models;
using MssqlDatabase.Repositories;

namespace IncognitoMessenger.Validation.Validators;

public class InviteValidator : AbstractValidator<InviteValidationModel>
{
    

    public InviteValidator(InviteRepository inviteRepository, ChatRepository chatRepository)
    {
        Invite? inviteRep = null;

        RuleFor(invite => invite)
            .Must(invite => (inviteRep = inviteRepository.Get(invite.Code)) != null)
            .WithMessage("Token is invalid or used already");

        RuleFor(invite => invite)
            .Must(invite => chatRepository.GetChats(invite.UserId).SingleOrDefault(chat => chat.Id == inviteRep?.ChatId) == null)
            .WithMessage("You member of this chat already");
    }
}