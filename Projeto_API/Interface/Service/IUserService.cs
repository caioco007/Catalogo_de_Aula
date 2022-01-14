using Projeto_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto_API.Interface.Repositorio
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<User> GetByIdAsync(int id);
        public Task<User> CreateAsync(User user);
        public Task<User> UpdateAsync(User user);
        public Task DeleteAsync(int id);
        Task<User> IsExistBdAsync(string nome, string senha);
        public Task<bool> RolesADExistsAsync();
    }
}
