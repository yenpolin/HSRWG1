using Accounting.Infrastructure.DataAccess;
using Accounting.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Application.Queries
{
    public class ItemQueries
    {

        private readonly DataContext _context;
        public ItemQueries(DataContext context)
        {
            _context = context;
        }

        public List<Item> Get()
        {
            var items = _context.Items.OrderBy(x => x.Order).AsNoTracking();
            return (items.ToList());
        }
    }
}
