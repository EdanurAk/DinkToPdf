using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class Compan
    {
        [Key]
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string Adress { get; set; }
        public string WebSite { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNu { get; set; }
    }
}
