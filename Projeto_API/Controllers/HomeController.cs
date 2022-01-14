using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_API.Interface.Repositorio;
using Projeto_API.Models;
using Projeto_API.Repositories;
using Projeto_API.Services;
using System;
using System.Threading.Tasks;

namespace Projeto_API.Controller
{
    [Route("api/v1/account")]
    public class HomeController : ControllerBase
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/v1/account/login
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> AuthenticateUser([FromBody] User model)
        {
            var user = await _userService.IsExistBdAsync(model.Username, model.Password);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            user.Password = "";
            return new
            {
                user = user,
                token = token
            };
        }

        // POST: api/v1/account/create
        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> PostUser(User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            bool rolesExist = await _userService.RolesADExistsAsync();

            if (rolesExist && model.Role == "Administrador")
            {
                return NotFound();
            }

            if (model.Role == "") model.Role = "Usuario";

            var userCreated = await _userService.CreateAsync(model);

            return Ok(userCreated);
        }

        // PUT: api/v1/account/update/5
        [HttpPut]
        [Route("update/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> PutUser(int id, User model)
        {

            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }


            bool rolesExist = await _userService.RolesADExistsAsync();

            if (rolesExist && model.Role == "Administrador")
            {
                return NotFound();
            }

            if (model.Role == "") model.Role = "Usuario";

            try
            {
                var userEdit = await _userService.UpdateAsync(model);

                return Ok(userEdit);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(409);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
        }


    }
}
