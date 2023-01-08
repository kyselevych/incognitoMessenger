using Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MssqlDatabase
{
    public class ApplicationContext : DbContext
    {
        private readonly string? stringConnection;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Chat> Chats { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<Invite> Invites { get; set; } = null!;
        public DbSet<Member> Members { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        public ApplicationContext(IConfiguration configuration)
        {
            stringConnection = configuration.GetConnectionString("MssqlDb");

            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(stringConnection);
        }
    }
}
