using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STIVEgroupe4API.Entity;
using STIVEgroupe4API.Service;

namespace STIVEgroupe4API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandeFournisseurController : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public ActionResult<IEnumerable<CommandeFournisseur>> GetAllCommandeFournisseur()
        {
            return new OkObjectResult(NegoSudService.GetAllCommmandeFournisseur());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{idCommandeFournisseur}")]
        public ActionResult<CommandeFournisseur> GetOneCommandeFournisseur(int idCommandeFournisseur)
        {
            #region Tests

            if (idCommandeFournisseur <= 0)
                return new BadRequestObjectResult("L'id de la commande fournisseur est négatif ou null.");

            #endregion

            return new OkObjectResult(NegoSudService.GetOneCommandeFournisseur(idCommandeFournisseur));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("create")]
        public ActionResult<CommandeFournisseur> CreateOneCommandeFournisseur([FromBody] CommandeFournisseur commandeFournisseur)
        {
            #region Tests

            if (commandeFournisseur.IdCommandeFournisseur < 0)
                return new BadRequestObjectResult("L'id de la commande fournisseur est négatif ou null.");

            #endregion

            return new OkObjectResult(NegoSudService.CreateOneCommandeFournisseur(commandeFournisseur));
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("update")]
        public ActionResult<CommandeFournisseur> UpdateOneCommandeFournisseur([FromBody] CommandeFournisseur commandeFournisseur)
        {
            #region Tests

            if (commandeFournisseur.IdCommandeFournisseur < 0)
                return new BadRequestObjectResult("L'id de la commande fournisseur est négatif ou null.");

            #endregion

            return new OkObjectResult(NegoSudService.UpdateOneCommandeFournisseur(commandeFournisseur));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("delete/{idCommandeFournisseur}")]
        public ActionResult<CommandeFournisseur> DeleteOneCommandeFournisseur(int idCommandeFournisseur)
        {
            #region Tests

            if (idCommandeFournisseur < 0)
                return new BadRequestObjectResult("L'id de la commande fournisseur est négatif ou null.");

            #endregion

            return new OkObjectResult(NegoSudService.DeleteOneCommandeFournisseur(idCommandeFournisseur));
        }
    }
}

