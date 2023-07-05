using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiShop.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public string ProductSize { get; set; }
        public string ProductColor { get; set; }
        public double ProductPrice { get; set; }

        [ForeignKey("categories")]
        public int ProductCategories { get; set; }


        [NotMapped]
        public IList<IFormFile> clientFile { get; set; }
        public string? PrimaryImg { get; set; }
        public string? img2_name { get; set; }
        public string? img3_name { get; set; }




        public DateTime ProductCreation { get; set; } = DateTime.Now;


    }
}
