using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthentificationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IJwtService _jwtService;

        public AuthentificationController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<int>> roleManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Vérifier si l'utilisateur existe déjà
            var existe = await _userManager.FindByNameAsync(dto.UserName);
            if (existe != null)
                return BadRequest("Nom d'utilisateur déjà utilisé.");

            // Vérifier la confirmation du mot de passe
            if (dto.Password != dto.ConfirmedPassword)
                return BadRequest("Les mots de passe ne correspondent pas.");

            // Créer l'utilisateur
            var utilisateur = new User
            {
                UserName = dto.UserName,
                Fullname = dto.Fullname
            };

            var resultat = await _userManager.CreateAsync(utilisateur, dto.Password);

            if (!resultat.Succeeded)
                return BadRequest(resultat.Errors);

            // Assigner le rôle
            await _userManager.AddToRoleAsync(utilisateur, "User");

            return Ok("Utilisateur créé avec succès.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Vérifier si le nom d'utilisateur existe
            var utilisateur = await _userManager.FindByNameAsync(dto.UserName);

            if (utilisateur == null)
                return Unauthorized("Identifiants invalides.");

            // Vérifier si le mot de passe correspond
            var resultat = await _signInManager.CheckPasswordSignInAsync(utilisateur, dto.Password, false);

            if (!resultat.Succeeded)
                return Unauthorized("Identifiants invalides.");

            var token = await _jwtService.GenererTokenAsync(utilisateur);
            return Ok(new { Token = token });
        }
    }
}
