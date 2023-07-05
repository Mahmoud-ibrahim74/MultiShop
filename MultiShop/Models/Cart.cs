using System.ComponentModel.DataAnnotations;

namespace MultiShop.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string ProductSize { get; set; }
        public string ProductColor { get; set; }
        public int Amount { get; set; }

    }
}
