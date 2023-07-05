using System.ComponentModel.DataAnnotations;

namespace MultiShop.Models
{
    public class ReviewsProducts
    {
        [Key]
        public int RevId { get; set; }
        public string RevDescription { get; set; }
        public string Name { get; set; }
        public string email { get; set; }
        public DateTime RevCreation { get; set; } = DateTime.Now;
        public int prod_categ { get; set; } 
    }
}
