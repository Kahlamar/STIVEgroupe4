using Microsoft.AspNetCore.Mvc;
using STIVEgroupe4API.Entity;
using STIVEgroupe4API.Service;

namespace STIVEgroupe4API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandeClientController : ControllerBase
    { 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public ActionResult<IEnumerable<CommandeClient>> GetAllCommandeClient()
        {
            return new OkObjectResult(NegoSudService.GetAllCommandeClient());
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{idCommandeClient:int}")]
        public ActionResult<CommandeClient> GetOneCommandeClient(int idCommandeClient)
        {
            #region Tests

            if (idCommandeClient <= 0)
                return new BadRequestObjectResult("L'id de la commande client est négatif ou null.");

            #endregion
            
            return new OkObjectResult(NegoSudService.GetOneCommandeClient(idCommandeClient));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("commandes_client/{idClient:int}")]
        public ActionResult<CommandeClient> GetAllCommandesClientById(int idClient)
        {
            #region Tests

            if (idClient <= 0)
                return new BadRequestObjectResult("L'id de la commande client est négatif ou null.");

            #endregion
            
            return new OkObjectResult(NegoSudService.GetAllCommandesByClient(idClient));
        }
        
        
        
        
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("create")]
        public ActionResult<CommandeClient> CreateOneCommandeClient([FromBody] CommandeClient commandeClient)
        {
            #region Tests

            if (commandeClient.IdCommandeClient < 0)
                return new BadRequestObjectResult("L'id de la commande client est négatif ou null.");

            #endregion
            
            return new OkObjectResult(NegoSudService.CreateOneCommandeClient(commandeClient));
        }
        
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("update")]
        public ActionResult<CommandeClient> UpdateOneCommandeClient([FromBody] CommandeClient commandeClient)
        {
            #region Tests

            if (commandeClient.IdCommandeClient < 0)
                return new BadRequestObjectResult("L'id de la commande client est négatif ou null.");

            #endregion
            
            return new OkObjectResult(NegoSudService.UpdateOneCommandeClient(commandeClient));
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("delete/{idCommandeClient:int}")]
        public ActionResult<CommandeClient> DeleteOneCommandeClient(int idCommandeClient)
        {
            #region Tests

            if (idCommandeClient < 0)
                return new BadRequestObjectResult("L'id de la commande client est négatif ou null.");

            #endregion
            
            return new OkObjectResult(NegoSudService.DeleteOneCommandeClient(idCommandeClient));
        }
    }
}
