using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiShop.Models
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? CategoryDescription { get; set; }

        [NotMapped]
        public IFormFile clientFile { get; set; }

        public byte[]? CategoryImg { get; set; }
        public DateTime CategoriesDate { get; set; } = DateTime.Now;  
        [NotMapped]
        public string? imageSrc
        {
            get
            {
                if (CategoryImg != null)
                {
                    return "data:image/jpg;base64," + Convert.ToBase64String(CategoryImg, 0, CategoryImg.Length);
                }
                else
                {
                    return string.Empty;
                }

            }
        }



    }
}
