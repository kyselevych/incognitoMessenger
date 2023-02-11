using Business.Entities;
using Microsoft.EntityFrameworkCore;
using MssqlDatabase;

namespace IncognitoMessenger.ApplicationCore.Chats;

public class MessageRepository
{
    private readonly DatabaseContext context;

    public MessageRepository(DatabaseContext context)
    {
        this.context = context;
    }

    public IEnumerable<Message> GetMessages(int chatId)
    {
        return context.Messages.Where(x => x.ChatId == chatId).OrderBy(x => x.DateTime).Include(x => x.User).ToList();
    }

    public Message? GetLastMessage(int chatId)
    {
        return context.Messages.Where(x => x.ChatId == chatId).OrderBy(x => x.DateTime).Include(x => x.User).LastOrDefault();
    }

    public Message Insert(Message message)
    {
        var newMessage = context.Messages.Add(message);
        context.SaveChanges();
        return context.Messages.Where(x => x.Id == newMessage.Entity.Id).Include(x => x.User).Single();
    }

    public void Delete(int id)
    {
        var message = new Message() { Id = id };
        context.Messages.Attach(message);
        context.Messages.Remove(message);
        context.SaveChanges();
    }
}