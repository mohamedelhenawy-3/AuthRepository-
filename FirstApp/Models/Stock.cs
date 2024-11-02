using System.ComponentModel.DataAnnotations.Schema;

namespace FirstApp.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Purchase { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDev { get; set; }   //divident "ارباح"
        public string Industry { get; set; } = string.Empty;
        public long MaketCap { get; set; }

        public List<Comment> Comments = new List<Comment>();

    }
}
