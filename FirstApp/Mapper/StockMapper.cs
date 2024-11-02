using FirstApp.DTO.Stock;
using FirstApp.Models;

namespace FirstApp.Mapper
{
    public static class StockMapper
    {
        public static Stock ToStockFromDTO(this StockDto dto)
        {
            return new Stock
            {
                Symbol = dto.Symbol,
                CompanyName = dto.CompanyName,
                Purchase = dto.Purchase,
                LastDev = dto.LastDev,
                Industry = dto.Industry

            };
        }
    }
}
