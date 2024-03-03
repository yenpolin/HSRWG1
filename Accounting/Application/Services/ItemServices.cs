using Accounting.Infrastructure.DataAccess;
using Accounting.Models;
using Microsoft.AspNetCore.Mvc;
using static Accounting.Application.Services.ValueServices;

namespace Accounting.Application.Services
{
    public class ItemServices
    {

        private readonly DataContext _context;
        public ItemServices(DataContext context)
        {
            _context = context;
        }

        public Item New(ItemParams @params)
        {
            var item = new Item
            {
                Name = @params.Name,
                Positive = @params.Positive,
                Order = @params.Order
            };
            _context.Items.Add(item);
            _context.SaveChanges();
            return item;
        }

        public Item ChangeOrder(OrderParams @orderParams)
        {
            var item= _context.Items.Find(@orderParams.ChangeId);
            var originalOrder = item.Order;
            var newOrder = orderParams.NewOrder;
            item.Order = newOrder;

            if (originalOrder < newOrder)
            {
                var itemsToChange = _context.Items.Where(i => i.Order > originalOrder && i.Order <= newOrder).ToList(); 
                foreach (var itemToChange in itemsToChange) {
                    itemToChange.Order--;
                }
            }

            if (originalOrder > newOrder)
            {
                var itemsToChange = _context.Items.Where(i => i.Order < originalOrder && i.Order >= newOrder).ToList();
                foreach (var itemToChange in itemsToChange)
                {
                    itemToChange.Order++;
                }
            }

            _context.SaveChanges();

            return item;
        }

        public class ItemParams
        {
            public string Name { get; set; }

            public bool Positive { get; set; }

            public int Order { get; set; }
        }

        public class OrderParams
        {
            public int ChangeId { get; set; }
            public int NewOrder { get; set; }
        }

    }


}
