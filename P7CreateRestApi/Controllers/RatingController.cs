using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services.Interfaces;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _service;

        public RatingController(IRatingService service)
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

        [HttpPost]
        public async Task<IActionResult> Create(RatingCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultat = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(Get), new { id = resultat.Id }, resultat);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RatingUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultat = await _service.UpdateAsync(id, dto);

            if (resultat == null)
                return NotFound();

            return Ok(resultat);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultat = await _service.DeleteAsync(id);

            if (!resultat)
                return NotFound();

            return NoContent();
        }
    }
}