using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services.Interfaces;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BidListController : ControllerBase
    {
        private readonly IBidListService _service;

        public BidListController(IBidListService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll ()
        {
            return Ok();
        }

        [HttpGet("{id]")]
        public IActionResult Get (int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Create(BidListCreateDTO dto)
        {
            return Ok();
        }

        [HttpPut("{id]")]
        public IActionResult Update (BidListUpdateDTO dto)
        {
            return Ok();
        }

        [HttpDelete("{id]")]
        public IActionResult Delete (int id)
        {
            return Ok();
        }
    }
}