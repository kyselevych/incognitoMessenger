using Business.Entities;
using FluentValidation.Results;
using IncognitoMessenger.ApplicationCore.Chats.Models;
using IncognitoMessenger.Validation.Validators;

namespace IncognitoMessenger.ApplicationCore.Chats
{
    public class ChatService
    {
        private readonly ChatRepository chatRepository;
        private readonly MessageRepository messageRepository;
        private readonly MemberValidator memberValidator;
        private readonly OwnerValidator ownerValidator;
        private readonly InviteRepository inviteRepository;

        public ChatService(
            ChatRepository chatRepository, 
            MessageRepository messageRepository, 
            MemberValidator memberValidator,
            OwnerValidator ownerValidator,
            InviteRepository inviteRepository
        )
        {
            this.chatRepository = chatRepository;
            this.messageRepository = messageRepository;
            this.memberValidator = memberValidator;
            this.ownerValidator = ownerValidator;
            this.inviteRepository = inviteRepository;
        }

        public Chat CreateChat(Chat data)
        {
            return chatRepository.Insert(data);
        }

        public IEnumerable<ChatCard> GetChats(int userId)
        {
            var chatList = new List<ChatCard>();
            var chats = chatRepository.GetChats(userId);

            foreach (var chat in chats)
            {
                var lastMessage = messageRepository.GetLastMessage(chat.Id);
                var chatListItem = new ChatCard() { Chat = chat, LastMessage = lastMessage };
                chatList.Add(chatListItem);
            }

            return chatList;
        }

        public Chat? GetChat(int userId, int chatId)
        {
            var validationResult = memberValidator.Validate(new Member { ChatId = chatId, UserId = userId });
            CheckValidationResult(validationResult);
            return chatRepository.GetChat(chatId);
        }

        public IEnumerable<Message> GetMessages(int chatId, int userId)
        {
            var validationResult = memberValidator.Validate(new Member { ChatId = chatId, UserId = userId });
            CheckValidationResult(validationResult);
            return messageRepository.GetMessages(chatId);
        }

        public Message SaveMessage(Message message)
        {
            var validationResult = memberValidator.Validate(new Member { ChatId = message.ChatId, UserId = message.UserId });
            CheckValidationResult(validationResult);
            return messageRepository.Insert(message);
        }

        public void DeleteMessage(Message message)
        {
            var validationResult = memberValidator.Validate(new Member { ChatId = message.ChatId, UserId = message.UserId });
            CheckValidationResult(validationResult);
            messageRepository.Delete(message.Id);
        }

        public void DeleteChat(int chatId, int userId)
        {
            var validationResult = ownerValidator.Validate(new Member { ChatId = chatId, UserId = userId });
            CheckValidationResult(validationResult);
            chatRepository.Delete(chatId);
        }

        public Member JoinUser(int userId, int chatId)
        {
            return chatRepository.AddUser(userId, chatId);
        }

        public void KickUser(int chatId, int userOwnerId, int userCandidateId)
        {
            var ownerValidationResult = ownerValidator.Validate(new Member { ChatId = chatId, UserId = userOwnerId });
            CheckValidationResult(ownerValidationResult);

            var memberValidationResult = memberValidator.Validate(new Member { ChatId = chatId, UserId = userCandidateId });
            CheckValidationResult(memberValidationResult);

            chatRepository.DeleteUser(userCandidateId, chatId);
        }

        public void LeaveChat(int chatId, int userId)
        {
            var validationResult = memberValidator.Validate(new Member { ChatId = chatId, UserId = userId });
            CheckValidationResult(validationResult);

            chatRepository.DeleteUser(userId, chatId);
        }


        private void CheckValidationResult(ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);
        }
    }
}
