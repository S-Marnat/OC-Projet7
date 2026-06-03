using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ILogger<AuthentificationController> _logger;

        public AuthentificationController(UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<IdentityRole<int>> roleManager, IJwtService jwtService, ILogger<AuthentificationController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            _logger.LogInformation("Tentative d'inscription d'un utilisateur.");
            
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Echec de l'inscription de l'utilisateur : modèle invalide.");
                return BadRequest("Les informations fournies pour l'utilisateur sont invalides.");
            }

            // Vérifier si l'utilisateur existe déjà
            var existe = await _userManager.FindByNameAsync(dto.UserName);
            if (existe != null)
            {
                _logger.LogWarning("Echec de l'inscription de l'utilisateur : nom d'utilisateur déjà utilisé.");
                return BadRequest("Nom d'utilisateur déjà utilisé.");
            }

            // Vérifier la confirmation du mot de passe
            if (dto.Password != dto.ConfirmedPassword)
            {
                _logger.LogWarning("Echec de l'inscription de l'utilisateur : les mots de passe ne correspondent pas.");
                return BadRequest("Les mots de passe ne correspondent pas.");
            }

            try
            {
                // Créer l'utilisateur
                var utilisateur = new User
                {
                    UserName = dto.UserName,
                    Fullname = dto.Fullname
                };

                var resultat = await _userManager.CreateAsync(utilisateur, dto.Password);

                if (!resultat.Succeeded)
                {
                    _logger.LogWarning("Echec de l'inscription de l'utilisateur : erreurs de validation.");
                    return BadRequest("Les informations fournies pour l'utilisateur sont invalides.");
                }

                // Assigner le rôle
                await _userManager.AddToRoleAsync(utilisateur, "User");

                _logger.LogInformation("L'utilisateur a été inscrit avec succès.");
                return Ok("Utilisateur créé avec succès.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'inscription de l'utilisateur");
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            _logger.LogInformation("Tentative de connexion d'un utilisateur.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Echec de la connexion de l'utilisateur : modèle invalide.");
                return BadRequest("Les informations fournies pour l'utilisateur sont invalides.");
            }

            // Vérifier si le nom d'utilisateur existe
            var utilisateur = await _userManager.FindByNameAsync(dto.UserName);

            if (utilisateur == null)
            {
                _logger.LogWarning("Echec de la connexion de l'utilisateur : nom d'utilisateur invalide.");
                return Unauthorized("Identifiants invalides.");
            }

            // Vérifier si le mot de passe correspond
            var resultat = await _signInManager.CheckPasswordSignInAsync(utilisateur, dto.Password, false);

            if (!resultat.Succeeded)
            {
                _logger.LogWarning("Echec de la connexion de l'utilisateur : mot de passe invalide.");
                return Unauthorized("Identifiants invalides.");
            }

            try
            {
                var token = await _jwtService.GenererTokenAsync(utilisateur);
                _logger.LogInformation("L'utilisateur s'est connecté avec succès.");
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la connexion de l'utilisateur");
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }
    }
}
