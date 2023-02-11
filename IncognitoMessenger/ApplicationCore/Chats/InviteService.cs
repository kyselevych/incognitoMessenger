using Business.Entities;
using FluentValidation.Results;
using IncognitoMessenger.ApplicationCore.Chats.Models;
using IncognitoMessenger.Validation.Validators;

namespace IncognitoMessenger.ApplicationCore.Chats
{
    public class InviteService
    {
        private readonly InviteRepository inviteRepository;
        private readonly InviteValidator inviteValidor;
        private readonly OwnerValidator ownerValidator;
        private readonly ChatRepository chatRepository;

        public InviteService(InviteRepository inviteRepository, InviteValidator inviteValidor, OwnerValidator ownerValidator, ChatRepository chatRepository)
        {
            this.inviteRepository = inviteRepository;
            this.inviteValidor = inviteValidor;
            this.ownerValidator = ownerValidator;
            this.chatRepository = chatRepository;
        }

        public Invite CreateInvite(int chatId, int userId)
        {
            var validationResult = ownerValidator.Validate(new Member { ChatId = chatId, UserId = userId });
            if (!validationResult.IsValid) throw new ValidationException(validationResult);

            var invite = new Invite() 
            { 
                ChatId = chatId,
                UserId = userId,
                Code = GenerateInviteKey(chatId)
            };

            return inviteRepository.Insert(invite);
        }

        public void AcceptInvite(int userId, string code)
        {
            var validationResult = inviteValidor.Validate(new InviteValidationModel { Code = code, UserId = userId });
            if (!validationResult.IsValid) throw new ValidationException(validationResult);

            var invite = inviteRepository.Get(code)!;
            chatRepository.AddUser(userId, invite.ChatId);
            inviteRepository.Delete(code);
        }

        private string GenerateInviteKey(int chatId)
        {
            return $"{chatId}-${Guid.NewGuid()}";
        }
    }
}
