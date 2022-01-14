using Projeto_API.Interface.Repositorio;
using Projeto_API.Interface.Service;
using Projeto_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto_API.Services
{
    public class AulaService : IAulaService
    {
        private readonly IAulaRepositorio _repository;

        public AulaService(IAulaRepositorio aulaRepositorio)
        {
            _repository = aulaRepositorio;
        }

        public async Task<AulaModel> CreateAsync(AulaModel aula)
        {
            return await _repository.CreateAsync(aula);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<AulaModel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<AulaModel> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> IsExistBdAsync(string nome, int id)
        {
            return await _repository.IsExistBdAsync(nome, id);
        }

        public async Task<AulaModel> UpdateAsync(AulaModel aula)
        {
            return await _repository.UpdateAsync(aula);
        }
    }
}
