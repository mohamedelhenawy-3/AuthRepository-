using System.ComponentModel.DataAnnotations.Schema;

namespace FirstApp.DTO.Stock
{
    public class StockDto
    {

        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDev { get; set; }   //divident "ارباح"
        public string Industry { get; set; } = string.Empty;
        public long MaketCap { get; set; }

    }
}
