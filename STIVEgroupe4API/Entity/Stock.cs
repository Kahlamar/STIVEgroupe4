using Microsoft.AspNetCore.Mvc;
using STIVEgroupe4API.Service;

namespace STIVEgroupe4API.Controllers;

public class Stock : Controller
{
    public class StockController : ControllerBase
    {
        

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public ActionResult<List<int>> GetStock()
        {
            return new OkObjectResult(NegoSudService.InventaireStock());
        }
        
        
    }
}