using Dot.Net.WebApi.Domain;
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

        public UserController(IUserService service)
        {
            _service = service;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultat = await _service.UpdateAsync(id, dto);

            if (resultat == null)
                return NotFound();

            return Ok(resultat);
        }
    }
}