using Projeto_API.Interface.Repositorio;
using Projeto_API.Interface.Service;
using Projeto_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto_API.Services
{
    public class ModuloService : IModuloService
    {
        private readonly IModuloRepositorio _repository;

        public ModuloService(IModuloRepositorio moduloRepositorio)
        {
            _repository = moduloRepositorio;
        }

        public async Task<ModuloModel> CreateAsync(ModuloModel modulo)
        {
            return await _repository.CreateAsync(modulo);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ModuloModel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ModuloModel> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<ModuloModel> IsExistBdAsync(string nome, int id)
        {
            return await _repository.IsExistBdAsync(nome.Trim(), id);
        }

        public async Task<ModuloModel> UpdateAsync(ModuloModel modulo)
        {
            return await _repository.UpdateAsync(modulo);
        }
    }
}
