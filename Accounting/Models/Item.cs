using System.Diagnostics.CodeAnalysis;

namespace Accounting.Models
{
    public class Item
    {
        public int Id { get; set; }

        
        public string Name { get; set; }

        public bool Positive { get; set; }

        public int Order { get; set; }
    }
}
