using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IUserService service, UserManager<User> userManager, ILogger<AdminController> logger)
        {
            _service = service;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPut("{id}/role")]
        public async Task<IActionResult> Update(int id, UserUpdateRoleDTO dto)
        {
            _logger.LogInformation("Tentative de modification du rôle pour l'utilisateur Id={Id}.", id);
            
            // Vérifier que l'utilisateur existe
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                _logger.LogWarning("La modification du rôle pour l'utilisateur Id={Id} a échoué : utilisateur non trouvé.", id);
                return NotFound("L'Id renseigné ne correspond à aucun utilisateur.");
            }

            try
            {
                // Supprimer les anciens rôles
                var oldRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, oldRoles);

                // Ajouter le nouveau rôle
                var newRole = dto.Role;
                _logger.LogInformation("Nouveau rôle demandé pour l'utilisateur Id={Id} : {Role}", id, newRole);
                var resultat = await _userManager.AddToRoleAsync(user, newRole);

                if (!resultat.Succeeded)
                {
                    _logger.LogWarning("Echec de la modification du rôle pour l'utilisateur Id={Id} : erreurs de validation.", id);
                    return BadRequest("Le rôle fourni est invalide.");
                }

                _logger.LogInformation("Le rôle de l'utilisateur Id={Id} a été modifié avec succès.", id);
                return Ok($"Le rôle de l'utilisateur {user.UserName} a été modifié avec succès.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la mise à jour du rôle pour l'utilisateur Id={Id}", id);
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }
    }
}
