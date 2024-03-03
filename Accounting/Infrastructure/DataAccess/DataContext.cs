using Accounting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace  Accounting.Infrastructure.DataAccess
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Value> Values { get; set; }

    }
}
