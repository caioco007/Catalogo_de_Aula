using Projeto_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto_API.Interface.Service
{
    public interface IAulaService
    {
        public Task<IEnumerable<AulaModel>> GetAllAsync();
        public Task<AulaModel> GetByIdAsync(int id);
        public Task<AulaModel> CreateAsync(AulaModel aula);
        public Task<AulaModel> UpdateAsync(AulaModel aula);
        public Task DeleteAsync(int id);
        public Task<bool> IsExistBdAsync(string nome, int id);
    }
}
