using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_API.Interface.Service;
using Projeto_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AulaApiController : ControllerBase
    {
        private readonly IAulaService _aulaService;

        public AulaApiController(IAulaService aulaService)
        {
            _aulaService = aulaService;
        }

        // GET: api/v1/AulaApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AulaModel>>> GetAulaModel()
        {
            return Ok(await _aulaService.GetAllAsync());
        }

        // GET: api/AulaApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AulaModel>> GetAulaModel(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var aula = await _aulaService.GetByIdAsync(id);

            if (aula == null)
            {
                return NotFound();
            }

            return Ok(aula);
        }

        // GET: api/v1/AulaApi/5
        [HttpPut("update/{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> PutAulaModel(int id, AulaModel aula)
        {
            if (id != aula.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(aula);
            }

            try
            {
                var aulaEdited = await _aulaService.UpdateAsync(aula);

                return Ok(aulaEdited);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(409);
            }
        }

        // POST: api/v1/AulaApi/create
        [HttpPost("create")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<AulaModel>> PostAulaModel([FromBody] AulaModel aula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(aula);
            }

            var aulaCreated = await _aulaService.CreateAsync(aula);

            return Ok(aulaCreated);
        }

        // DELETE: api/v1/AulaApi/delete/5
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteAulaModel(int id)
        {
            var aula = await _aulaService.GetByIdAsync(id);

            if (aula == null)
            {
                return NotFound();
            }

            await _aulaService.DeleteAsync(aula.Id);

            return Ok();
        }

        // DELETE: api/v1/AulaApi/IsExists/teste/5
        [HttpGet("IsExists/{nome}/{id}")]
        public async Task<ActionResult> GetAulaModel(string nome, int id)
        {
            return Ok(await _aulaService.IsExistBdAsync(nome, id));
        }

    }
}