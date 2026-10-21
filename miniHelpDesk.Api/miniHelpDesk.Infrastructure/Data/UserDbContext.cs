using Microsoft.EntityFrameworkCore;
using miniHelpDesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace miniHelpDesk.Infrastructure.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
