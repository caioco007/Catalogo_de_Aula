using Projeto_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto_API.Interface.Repositorio
{
    public interface IModuloRepositorio
    {
        public Task<IEnumerable<ModuloModel>> GetAllAsync();
        public Task<ModuloModel> GetByIdAsync(int id);
        public Task<ModuloModel> CreateAsync(ModuloModel modulo);
        public Task<ModuloModel> UpdateAsync(ModuloModel modulo);
        public Task DeleteAsync(int id);
        public Task<ModuloModel> IsExistBdAsync(string nome, int id);
    }
}
