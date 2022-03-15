using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STIVEgroupe4API.Entity;
using STIVEgroupe4API.Service;

namespace STIVEgroupe4API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FournisseurController : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public ActionResult<IEnumerable<Fournisseur>> GetAllFournisseur()
        {
            return new OkObjectResult(NegoSudService.GetAllFournisseurs());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{idFournisseur}")]
        public ActionResult<Fournisseur> GetOneFournisseur(int idFournisseur)
        {
            #region Tests

            if (idFournisseur <= 0)
                return new BadRequestObjectResult("L'id du fournisseur est négatif ou null.");

            #endregion

            return new OkObjectResult(NegoSudService.GetOneFournisseur(idFournisseur));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("create")]
        public ActionResult<Fournisseur> CreateOneFournisseur([FromBody] Fournisseur fournisseur)
        {
            #region Tests

            if (fournisseur.IdFournisseur < 0)
                return new BadRequestObjectResult("L'id du fournisseur est négatif ou null.");

            #endregion

            return new OkObjectResult(NegoSudService.CreateOneFournisseur(fournisseur));
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("update")]
        public ActionResult<CommandeFournisseur> UpdateOneFournisseur([FromBody] Fournisseur fournisseur)
        {
            #region Tests

            if (fournisseur.IdFournisseur < 0)
                return new BadRequestObjectResult("L'id du fournisseur est négatif ou null.");

            #endregion

            return new OkObjectResult(NegoSudService.UpdateOneFournisseur(fournisseur));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("delete/{idFournisseur}")]
        public ActionResult<CommandeFournisseur> DeleteOneFournisseur(int idFournisseur)
        {
            #region Tests

            if (idFournisseur < 0)
                return new BadRequestObjectResult("L'id du fournisseur est négatif ou null.");

            #endregion

            return new OkObjectResult(NegoSudService.DeleteOneFournisseur(idFournisseur));
        }
    }
}
