using Microsoft.AspNetCore.Mvc;
using STIVEgroupe4API.Entity;
using STIVEgroupe4API.Service;

namespace STIVEgroupe4API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        #region Clients

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public ActionResult<IEnumerable<Client>> GetAllClient()
        {
            return new OkObjectResult(NegoSudService.GetAllClient());
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{idClient}")]
        public ActionResult<Client> GetOneClient(int idClient)
        {
            #region Tests

            if (idClient <= 0)
                return new BadRequestObjectResult("L'id du client est négatif ou null.");

            #endregion
            
            return new OkObjectResult(NegoSudService.GetOneClient(idClient));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("create")]
        public ActionResult<Client> CreateOneClient([FromBody] Client client)
        {
            #region Tests

            if (client.IdClient < 0)
                return new BadRequestObjectResult("L'id du client est négatif ou null.");

            #endregion
            
            return new OkObjectResult(NegoSudService.CreateOneClient(client));
        }
        
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("update")]
        public ActionResult<Client> UpdateOneClient([FromBody] Client client)
        {
            #region Tests

            if (client.IdClient < 0)
                return new BadRequestObjectResult("L'id du client est négatif ou null.");

            #endregion
            
            return new OkObjectResult(NegoSudService.UpdateOneClient(client));
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("delete/{idClient}")]
        public ActionResult<Client> DeleteOneClient(int idClient)
        {
            #region Tests

            if (idClient < 0)
                return new BadRequestObjectResult("L'id du client est négatif ou null.");

            #endregion
            
            return new OkObjectResult(NegoSudService.DeleteOneClient(idClient));
        }

        #endregion
        
        
        
    }
}
