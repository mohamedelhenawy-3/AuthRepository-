using FirstApp.Data;
using FirstApp.DTO.Stock;
using FirstApp.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StockController : ControllerBase
    {
        private readonly AppDBContext _context;
        public StockController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllStocks()
        {
            var stocks = _context.Stocks.ToList();
            return Ok(stocks);
        }
        [HttpGet("{Id}")]
        public IActionResult GetStockById([FromRoute] int Id)
        {
            var stocks = _context.Stocks.Find(Id);
            return Ok(stocks);
        }

        [HttpPost]
        public IActionResult CreateStock([FromBody] StockDto newstockDTO)
        {
            var newStock = newstockDTO.ToStockFromDTO();
            _context.Stocks.Add(newStock);
            _context.SaveChanges();
            return Ok(newStock);
        }

       
        [HttpPut("{StockId}")]
        public IActionResult UpdateStock([FromRoute] int StockId, [FromBody]StockDto newstockDTO)
        {
            var newStock = _context.Stocks.FirstOrDefault(x => x.Id == StockId);
            newStock.Symbol = newstockDTO.Symbol;
            newStock.CompanyName = newstockDTO.CompanyName;
            newStock.Purchase = newstockDTO.Purchase;

            _context.SaveChanges();
            return Ok(newStock);
        }

        [HttpDelete("{StockId}")]
        public IActionResult DeleteStock([FromRoute] int StockId)
        {
            var newStock = _context.Stocks.FirstOrDefault(x => x.Id == StockId);
            _context.Remove(newStock);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
