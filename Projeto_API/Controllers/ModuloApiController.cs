using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_API.Interface.Service;
using Projeto_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ModuloApiController : ControllerBase
    {
        private readonly IModuloService _moduloService;

        public ModuloApiController(IModuloService moduloService)
        {
            _moduloService = moduloService;
        }

        // GET: api/v1/ModuloApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuloModel>>> GetModuloModel()
        {
            return Ok(await _moduloService.GetAllAsync());
        }

        // GET: api/v1/ModuloApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModuloModel>> GetModuloModelId(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var modulo = await _moduloService.GetByIdAsync(id);

            if (modulo == null)
            {
                return NotFound();
            }

            return Ok(modulo);
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> PutModuloModel(int id, ModuloModel modulo)
        {
            if (id != modulo.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(modulo);
            }

            try
            {
                var moduloEdited = await _moduloService.UpdateAsync(modulo);

                return Ok(moduloEdited);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(409);
            }
        }

        // POST: api/v1/ModuloApi/create
        [HttpPost("create")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<ModuloModel>> PostModuloModel(ModuloModel modulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modulo);
            }

            var moduloCreated = await _moduloService.CreateAsync(modulo);

            return Ok(moduloCreated);
        }

        // DELETE: api/v1/ModuloApi/delete/5
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteModuloModel(int id)
        {
            var modulo = await _moduloService.GetByIdAsync(id);

            if (modulo == null)
            {
                return BadRequest();
            }

            await _moduloService.DeleteAsync(modulo.Id);

            return Ok();
        }

        // DELETE: api/v1/ModuloApi/IsExists/teste/5
        [HttpGet("IsExists/{nome}/{id}")]
        public async Task<ActionResult<ModuloModel>> GetModuloModelNomeId(string nome, int id)
        {
            var modulo = await _moduloService.IsExistBdAsync(nome, id);

            if (modulo == null)
            {
                return NotFound();
            }

            return modulo;
        }

    }
}
