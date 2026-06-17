using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _service;
        private readonly ILogger<RatingController> _logger;

        public RatingController(IRatingService service, ILogger<RatingController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Tentative de lecture de l'ensemble des Rating");

            try
            {
                var resultat = await _service.GetAllAsync();
                _logger.LogInformation("L'ensemble des Rating a été lu avec succès.");
                return Ok(resultat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la lecture de l'ensemble des Rating");
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation("Tentative de lecture du Rating Id={Id}", id);

            try
            {
                var resultat = await _service.GetByIdAsync(id);

                if (resultat == null)
                {
                    _logger.LogWarning("La lecture du Rating Id={Id} a échoué : Rating non trouvé.", id);
                    return NotFound("L'Id renseigné ne correspond à aucun Rating.");
                }

                _logger.LogInformation("Le Rating Id={Id} a été lu avec succès.", id);
                return Ok(resultat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la lecture du Rating Id={Id}", id);
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(RatingCreateDTO dto)
        {
            _logger.LogInformation("Tentative de création d'un Rating");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Echec de la création du Rating : modèle invalide.");
                return BadRequest("Les informations fournies pour le Rating sont invalides.");
            }

            try
            {
                var resultat = await _service.CreateAsync(dto);

                _logger.LogInformation("Le Rating Id={Id} a été créé avec succès.", resultat.Id);
                return CreatedAtAction(nameof(Get), new { id = resultat.Id }, resultat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la création du Rating");
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RatingUpdateDTO dto)
        {
            _logger.LogInformation("Tentative de modification du Rating Id={Id}", id);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Echec de la modification du Rating Id={Id} : modèle invalide.", id);
                return BadRequest("Les informations fournies pour le Rating sont invalides.");
            }

            try
            {
                var resultat = await _service.UpdateAsync(id, dto);

                if (resultat == null)
                {
                    _logger.LogWarning("La modification du Rating Id={Id} a échoué : Rating non trouvé.", id);
                    return NotFound("L'Id renseigné ne correspond à aucun Rating.");
                }

                _logger.LogInformation("Le Rating Id={Id} a été modifié avec succès.", id);
                return Ok(resultat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la modification du Rating Id={Id}", id);
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Tentative de suppression du Rating Id={Id}", id);

            try
            {
                var resultat = await _service.DeleteAsync(id);

                if (!resultat)
                {
                    _logger.LogWarning("La suppression du Rating Id={Id} a échoué : Rating non trouvé.", id);
                    return NotFound("L'Id renseigné ne correspond à aucun Rating.");
                }

                _logger.LogInformation("Le Rating Id={Id} a été supprimé avec succès.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la suppression du Rating Id={Id}", id);
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }
    }
}
