using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class Payment
    {
        [Key]
        public int ID { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public DateTime DueDate { get; set; }
    }
}
