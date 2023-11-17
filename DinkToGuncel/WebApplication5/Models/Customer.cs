using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class Customer
    {
        [Key]
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string Adress { get; set; }
        public string Adress2 { get; set; }
        public string Adress3 { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNu { get; set; }
    }
}
