using Business.Entities;
using MssqlDatabase;

namespace IncognitoMessenger.ApplicationCore.Chats;

public class InviteRepository
{
    private readonly DatabaseContext context;

    public InviteRepository(DatabaseContext context)
    {
        this.context = context;
    }

    public Invite? Get(string code)
    {
        return context.Invites.Where(x => x.Code == code).SingleOrDefault();
    }

    public Invite Insert(Invite invite)
    {
        var newInvite = context.Invites.Add(invite);
        context.SaveChanges();
        return newInvite.Entity;
    }

    public void Delete(string code)
    {
        var invite = context.Invites.Where(x => x.Code == code).SingleOrDefault();

        if (invite == null) return;

        context.Invites.Remove(invite);
        context.SaveChanges();
    }
}