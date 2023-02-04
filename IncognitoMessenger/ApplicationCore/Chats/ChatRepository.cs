﻿using Business.Entities;
using MssqlDatabase;

namespace IncognitoMessenger.ApplicationCore.Chats;

public class ChatRepository
{
    private readonly DatabaseContext context;

    public ChatRepository(DatabaseContext context)
    {
        this.context = context;
    }

    public IEnumerable<Chat> GetChats(int userId)
    {
        return context.Members.Where(x => x.UserId == userId).Select(x => x.Chat).ToList();
    }

    public Member AddUser(int userId, int chatId)
    {
        var member = new Member() 
        { 
            UserId = userId,
            ChatId = chatId
        };

        var newMember = context.Members.Add(member);
        context.SaveChanges();
        return newMember.Entity;
    }

    public void DeleteUser(int userId, int chatId)
    {
        var member = context.Members.Where(x => x.UserId == userId && x.ChatId == chatId).First();
        context.Members.Remove(member);
        context.SaveChanges();
    }

    public Chat Insert(Chat chat)
    {
        var newChat = context.Chats.Add(chat);
        context.SaveChanges();
        return newChat.Entity;
    }

    public void Delete(int id)
    {
        var chat = new Chat() { Id = id };
        context.Chats.Attach(chat);
        context.Chats.Remove(chat);
        context.SaveChanges();
    }
}