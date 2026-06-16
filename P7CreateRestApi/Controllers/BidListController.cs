using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BidListController : ControllerBase
    {
        private readonly IBidListService _service;
        private readonly ILogger<BidListController> _logger;

        public BidListController(IBidListService service, ILogger<BidListController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll ()
        {
            _logger.LogInformation("Tentative de lecture de l'ensemble des BidList");

            try
            {
                var resultat = await _service.GetAllAsync();
                _logger.LogInformation("L'ensemble des BidList a été lu avec succès.");
                return Ok(resultat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la lecture de l'ensemble des BidList");
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get (int id)
        {
            _logger.LogInformation("Tentative de lecture du BidList Id={Id}", id);

            try
            {
                var resultat = await _service.GetByIdAsync(id);

                if (resultat == null)
                {
                    _logger.LogWarning("La lecture du BidList Id={Id} a échoué : BidList non trouvé.", id);
                    return NotFound("L'Id renseigné ne correspond à aucun BidList.");
                }

                _logger.LogInformation("Le BidList Id={Id} a été lu avec succès.", id);
                return Ok(resultat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la lecture du BidList Id={Id}", id);
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(BidListCreateDTO dto)
        {
            _logger.LogInformation("Tentative de création d'un BidList");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Echec de la création du BidList : modèle invalide.");
                return BadRequest("Les informations fournies pour le BidList sont invalides.");
            }

            try
            {
                var resultat = await _service.CreateAsync(dto);

                _logger.LogInformation("Le BidList Id={Id} a été créé avec succès.", resultat.BidListId);
                return CreatedAtAction(nameof(Get), new { id = resultat.BidListId }, resultat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la création du BidList");
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update (int id, BidListUpdateDTO dto)
        {
            _logger.LogInformation("Tentative de modification du BidList Id={Id}", id);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Echec de la modification du BidList Id={Id} : modèle invalide.", id);
                return BadRequest("Les informations fournies pour le BidList sont invalides.");
            }

            try
            {
                var resultat = await _service.UpdateAsync(id, dto);

                if (resultat == null)
                {
                    _logger.LogWarning("La modification du BidList Id={Id} a échoué : BidList non trouvé.", id);
                    return NotFound("L'Id renseigné ne correspond à aucun BidList.");
                }

                _logger.LogInformation("Le BidList Id={Id} a été modifié avec succès.", id);
                return Ok(resultat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la modification du BidList Id={Id}", id);
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            _logger.LogInformation("Tentative de suppression du BidList Id={Id}", id);

            try
            {
                var resultat = await _service.DeleteAsync(id);

                if (!resultat)
                {
                    _logger.LogWarning("La suppression du BidList Id={Id} a échoué : BidList non trouvé.", id);
                    return NotFound("L'Id renseigné ne correspond à aucun BidList.");
                }

                _logger.LogInformation("Le BidList Id={Id} a été supprimé avec succès.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la suppression du BidList Id={Id}", id);
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }
    }
}
