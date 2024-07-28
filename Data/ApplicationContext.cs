using Microsoft.EntityFrameworkCore;
using System;

namespace TestTask.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
    }
}
