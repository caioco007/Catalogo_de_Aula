using Microsoft.EntityFrameworkCore;
using Projeto_API.Interface.Repositorio;
using Projeto_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_API.Data.Repositorio
{
    public class UserRepositorio : IUserRepositorio
    {
        private readonly ModuloContext _context;

        public UserRepositorio(ModuloContext context)
        {
            _context = context;
        }

        public async Task<bool> RolesADExistsAsync()
        {
            var s = await _context.Users
                          .FirstOrDefaultAsync(e => e.Role == "Administrador");

            return s != null;
        }

        public async Task<User> CreateAsync(User user)
        {
            var newUser = _context.Add(user);
            await _context.SaveChangesAsync();

            return newUser.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                            .OrderBy(x => x.Username)
                            .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                        .OrderBy(x => x.Username)
                        .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<User> IsExistBdAsync(string nome, string senha)
        {
            return await _context.Users
                          .FirstOrDefaultAsync(x => x.Username == nome && x.Password == senha);
            
        }

        public async Task<User> UpdateAsync(User user)
        {
            var editAluno = _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return editAluno.Entity;
        }

    }
}
