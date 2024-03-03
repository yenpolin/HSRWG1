using Microsoft.EntityFrameworkCore;
using Accounting.Infrastructure.DataAccess;
using Accounting.Models;
using System.Text.Json;

namespace Accounting.Extensions
{
    public class DbInitializer
    {
        public void DbInitialize(DataContext dbContext) {
            dbContext.Database.EnsureCreated();
            if (!dbContext.Items.Any())
            {

                var itemData = File.ReadAllText("Infrastructure/Seed/ItemSeeds.json");
                var items = JsonSerializer.Deserialize<List<Item>>(itemData);

                if (items != null)
                {
                    foreach (var item in items)
                        dbContext.Items.Add(item);
                }
                dbContext.SaveChanges();
            }
        }
        
}
}
