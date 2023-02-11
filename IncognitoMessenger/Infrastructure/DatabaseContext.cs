using Business.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;

namespace MssqlDatabase
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Chat> Chats { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<Invite> Invites { get; set; } = null!;
        public DbSet<Member> Members { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
    }
}
