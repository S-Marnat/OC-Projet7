using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services.Interfaces;
using System.Data;

namespace Dot.Net.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService service, UserManager<User> userManager, ILogger<UserController> logger)
        {
            _service = service;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Tentative de lecture de l'ensemble des User");

            try
            {
                var resultat = await _service.GetAllAsync();
                _logger.LogInformation("L'ensemble des User a été lu avec succès.");
                return Ok(resultat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la lecture de l'ensemble des User");
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation("Tentative de lecture du User Id={Id}", id);

            try
            {
                var resultat = await _service.GetByIdAsync(id);

                if (resultat == null)
                {
                    _logger.LogWarning("La lecture du User Id={Id} a échoué : User non trouvé.", id);
                    return NotFound("L'Id renseigné ne correspond à aucun User.");
                }

                _logger.LogInformation("Le User Id={Id} a été lu avec succès.", id);
                return Ok(resultat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la lecture du User Id={Id}", id);
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }

        [HttpPut("profil")]
        public async Task<IActionResult> Update(UserUpdateDTO dto)
        {
            _logger.LogInformation("Tentative de modification du profil d'un User");

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                _logger.LogWarning("Echec de la modification du profil du User : utilisateur non authentifié.");
                return Unauthorized();
            }

            try
            {
                // Vérifier si le username change
                if (dto.UserName != null && dto.UserName != user.UserName)
                {
                    var existe = await _userManager.FindByNameAsync(dto.UserName);
                    if (existe != null)
                    {
                        _logger.LogWarning("La modification du profil du User Id={Id} a échoué : nom d'utilisateur déjà utilisé.", user.Id);
                        return BadRequest("Nom d'utilisateur déjà utilisé.");
                    }

                    user.UserName = dto.UserName;
                    user.NormalizedUserName = dto.UserName.ToUpper();
                }

                // Mettre à jour le fullname
                if (dto.Fullname != null)
                    user.Fullname = dto.Fullname;

            
                var resultat = await _userManager.UpdateAsync(user);

                if (!resultat.Succeeded)
                {
                    _logger.LogWarning("La modification du profil du User Id={Id} a échoué : erreur de validation.", user.Id);
                    return BadRequest("Les informations de profil fournies sont invalides.");
                }

                _logger.LogInformation("Le profil du User Id={Id} a été modifié avec succès.", user.Id);
                return Ok("Profil mis à jour avec succès.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la modification du profil du User Id={Id}", user.Id);
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }

        [HttpPut("password")]
        public async Task<IActionResult> UpdatePassword(UserUpdatePasswordDTO dto)
        {
            _logger.LogInformation("Tentative de modification du mot de passe d'un User");

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                _logger.LogWarning("Echec de la modification du mot de passe du User : utilisateur non authentifié.");
                return Unauthorized();
            }

            try
            {
                var resultat = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);

                if (!resultat.Succeeded)
                {
                    _logger.LogWarning("La modification du mot de passe du User Id={Id} a échoué : erreur de validation.", user.Id);
                    return BadRequest("Les informations de mot de passe fournies sont invalides.");
                }

                _logger.LogInformation("Le mot de passe du User Id={Id} a été modifié avec succès.", user.Id);
                return Ok("Mot de passe modifié avec succès.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la modification du mot de passe du User Id={Id}", user.Id);
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }

        [HttpDelete("compte")]
        public async Task<IActionResult> Delete()
        {
            _logger.LogInformation("Tentative de suppression d'un User");

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                _logger.LogWarning("Echec de la suppression du User : utilisateur non authentifié.");
                return Unauthorized();
            }

            try
            {
                var resultat = await _userManager.DeleteAsync(user);

                if (!resultat.Succeeded)
                {
                    _logger.LogWarning("La suppression du User Id={Id} a échoué : erreur de validation.", user.Id);
                    return BadRequest("Impossible de supprimer le compte.");
                }

                _logger.LogInformation("Le User Id={Id} a été supprimé avec succès.", user.Id);
                return Ok("Votre compte a été supprimé avec succès.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la suppression du User Id={Id}", user.Id);
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }
    }
}
