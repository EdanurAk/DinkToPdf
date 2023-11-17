using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class bill
    {
        [Key]
        public int ID { get; set; }
        public string OzNo { get; set; }
        public string BillName { get; set; }
        public string BillType { get; set; }
        public string BillNu { get; set; }
        public DateTime Date { get; set; }
    }
}
