using System.ComponentModel.DataAnnotations;

namespace MultiShop.Models
{
    public class ContactUs
    {
        [Key]
        public int MailId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
