using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly UserManager<User> _userManager;

        public AdminController(IUserService service, UserManager<User> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpPut("{id}/role")]
        public async Task<IActionResult> Update(int id, UserUpdateRoleDTO dto)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
                return NotFound();

            // Supprimer les anciens rôles
            var oldRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, oldRoles);

            // Ajouter le nouveau rôle
            var newRole = dto.Role.ToLower();
            var resultat = await _userManager.AddToRoleAsync(user, newRole);

            if (!resultat.Succeeded)
                return BadRequest(resultat.Errors);

            return Ok($"Le rôle de l'utilisateur {user.UserName} a été modifié avec succès.");
        }
    }
}
