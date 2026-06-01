using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services.Interfaces;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly UserManager<User> _userManager;

        public UserController(IUserService service, UserManager<User> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resultat = await _service.GetAllAsync();
            return Ok(resultat);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var resultat = await _service.GetByIdAsync(id);

            if (resultat == null)
                return NotFound();

            return Ok(resultat);
        }

        [HttpPut("profil")]
        public async Task<IActionResult> Update(UserUpdateDTO dto)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized();

            // Vérifier si le username change
            if (dto.UserName != null && dto.UserName != user.UserName)
            {
                var existe = await _userManager.FindByNameAsync(dto.UserName);
                if (existe != null)
                    return BadRequest("Nom d'utilisateur déjà utilisé.");

                user.UserName = dto.UserName;
                user.NormalizedUserName = dto.UserName.ToUpper();
            }

            // Mettre à jour le fullname
            if (dto.Fullname != null)
                user.Fullname = dto.Fullname;

            var resultat = await _userManager.UpdateAsync(user);

            if (!resultat.Succeeded)
                return BadRequest(resultat.Errors);

            return Ok("Profil mis à jour avec succès.");
        }

        [HttpPut("password")]
        public async Task<IActionResult> UpdatePassword(UserUpdatePasswordDTO dto)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized();

            var resultat = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);

            if (!resultat.Succeeded)
                return BadRequest(resultat.Errors);

            return Ok("Mot de passe modifié avec succès.");
        }

        [HttpDelete("compte")]
        public async Task<IActionResult> Delete()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized();

            var resultat = await _userManager.DeleteAsync(user);

            if (!resultat.Succeeded)
                return BadRequest(resultat.Errors);

            return Ok("Votre compte a été supprimé avec succès.");
        }
    }
}