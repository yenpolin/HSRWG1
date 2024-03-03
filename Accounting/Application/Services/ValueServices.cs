using Accounting.Infrastructure.DataAccess;
using Accounting.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoonCakeOrder.Application.ClientModels;

namespace Accounting.Application.Services
{
    public class ValueServices
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ValueServices(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<Value> Get(){
            var values=_context.Values.ToList();
            return values;
        }

        public List<ValueClientModel> GetByYearMonth(DateTime YearMonth){
            var values=_context.Values.Where(v => v.YearMonth.Year == YearMonth.Year && v.YearMonth.Month == YearMonth.Month)
                .Include(x => x.Item)
                .OrderBy(x => x.Item.Order)
                .ToList();
            return _mapper.Map<List<ValueClientModel>>(values);
        }

        public Value New(ValueParams @params)
        {
            var value = new Value
            {
                YearMonth = @params.YearMonth,
                ItemId= @params.ItemId,
                Amount= @params.Amount,
            };
            _context.Values.Add(value);
            _context.SaveChanges();
            return value;
        }

        public void NewMany(DateTime yearMonth, List<ItemValue> @params)
        {
            foreach (var param in @params)
            {
                var valueToUpdate = _context.Values.Where(v => v.ItemId == param.Value.ItemId).FirstOrDefault();
                if (valueToUpdate == null)
                {
                var value = new Value
                {
                    YearMonth = yearMonth,
                    ItemId = param.Item.Id,
                    Amount = param.Value.Amount              
                };
                _context.Values.Add(value);
                }
                else{
                    valueToUpdate.Amount = param.Value.Amount;
                }
            }
            _context.SaveChanges();
            
        }

        public class ValueParams
        {
            public DateTime YearMonth { get; set; }
            public int ItemId { get; set; }
            public int Amount { get; set; }

        }

        public class ItemValue
        {
            public Item Item { get; set; }
            public ValueClientModel Value { get; set; }
        }

    }


}
