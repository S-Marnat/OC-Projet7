using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services.Interfaces;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _service;
        private readonly ILogger<TradeController> _logger;

        public TradeController(ITradeService service, ILogger<TradeController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Tentative de lecture de l'ensemble des Trade");

            try
            {
                var resultat = await _service.GetAllAsync();
                _logger.LogInformation("L'ensemble des Trade a été lu avec succès.");
                return Ok(resultat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la lecture de l'ensemble des Trade");
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation("Tentative de lecture du Trade Id={Id}", id);

            try
            {
                var resultat = await _service.GetByIdAsync(id);

                if (resultat == null)
                {
                    _logger.LogWarning("La lecture du Trade Id={Id} a échoué : Trade non trouvé.", id);
                    return NotFound("L'Id renseigné ne correspond à aucun Trade.");
                }

                _logger.LogInformation("Le Trade Id={Id} a été lu avec succès.", id);
                return Ok(resultat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la lecture du Trade Id={Id}", id);
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(TradeCreateDTO dto)
        {
            _logger.LogInformation("Tentative de création d'un Trade");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Echec de la création du Trade : modèle invalide.");
                return BadRequest("Les informations fournies pour le Trade sont invalides.");
            }

            try
            {
                var resultat = await _service.CreateAsync(dto);

                _logger.LogInformation("Le Trade Id={Id} a été créé avec succès.", resultat.TradeId);
                return CreatedAtAction(nameof(Get), new { id = resultat.TradeId }, resultat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la création du Trade");
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TradeUpdateDTO dto)
        {
            _logger.LogInformation("Tentative de modification du Trade Id={Id}", id);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Echec de la modification du Trade Id={Id} : modèle invalide.", id);
                return BadRequest("Les informations fournies pour le Trade sont invalides.");
            }

            try
            {
                var resultat = await _service.UpdateAsync(id, dto);

                if (resultat == null)
                {
                    _logger.LogWarning("La modification du Trade Id={Id} a échoué : Trade non trouvé.", id);
                    return NotFound("L'Id renseigné ne correspond à aucun Trade.");
                }

                _logger.LogInformation("Le Trade Id={Id} a été modifié avec succès.", id);
                return Ok(resultat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la modification du Trade Id={Id}", id);
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Tentative de suppression du Trade Id={Id}", id);

            try
            {
                var resultat = await _service.DeleteAsync(id);

                if (!resultat)
                {
                    _logger.LogWarning("La suppression du Trade Id={Id} a échoué : Trade non trouvé.", id);
                    return NotFound("L'Id renseigné ne correspond à aucun Trade.");
                }

                _logger.LogInformation("Le Trade Id={Id} a été supprimé avec succès.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la suppression du Trade Id={Id}", id);
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }
    }
}
