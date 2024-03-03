

using System.ComponentModel.DataAnnotations;

namespace Accounting.Models
{
    public class Value
    {
        public int Id { get; set; }
        public DateTime YearMonth { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int Amount { get; set; }

        [Timestamp]
        public DateTime Updated { get; set; }
    }
}
