using Microsoft.EntityFrameworkCore;
using miniHelpDesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace miniHelpDesk.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
