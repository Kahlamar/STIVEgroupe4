using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STIVEgroupe4API.Entity;
using STIVEgroupe4API.Service;

namespace STIVEgroupe4API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VinController : ControllerBase
    {
        

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public ActionResult<IEnumerable<Vin>> GetAllVin()
        {
            return new OkObjectResult(NegoSudService.GetAllVin());
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{idVin}")]
        public ActionResult<Vin> GetOneVin(int idVin)
        {
            #region Tests

            if (idVin <= 0)
                return new BadRequestObjectResult("L'id du vin est négatif ou null.");

            #endregion
            
            return new OkObjectResult(NegoSudService.GetOneVin(idVin));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("create")]
        public ActionResult<Vin> CreateOneVin([FromBody] Vin vin)
        {
            #region Tests

            if (vin.IdVin < 0)
                return new BadRequestObjectResult("L'id du vin est négatif ou null.");

            #endregion
            
            return new OkObjectResult(NegoSudService.CreateOneVin(vin));
        }
        
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("update")]
        public ActionResult<Vin> UpdateOneVin([FromBody] Vin vin)
        {
            #region Tests

            if (vin.IdVin < 0)
                return new BadRequestObjectResult("L'id du vin est négatif ou null.");

            #endregion
            
            return new OkObjectResult(NegoSudService.UpdateOneVin(vin));
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("delete/{idClient}")]
        public ActionResult<Vin> DeleteOneVin(int idVin)
        {
            #region Tests

            if (idVin < 0)
                return new BadRequestObjectResult("L'id du client est négatif ou null.");

            #endregion
            
            return new OkObjectResult(NegoSudService.DeleteOneVin(idVin));
        }

        
    }
}
