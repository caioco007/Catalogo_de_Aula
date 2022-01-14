using Projeto_API.Interface.Repositorio;
using Projeto_API.Interface.Service;
using Projeto_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto_API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepositorio _repository;

        public UserService(IUserRepositorio UserRepositorio)
        {
            _repository = UserRepositorio;
        }

        public async Task<User> CreateAsync(User user)
        {
            return await _repository.CreateAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<User> IsExistBdAsync(string nome, string senha)
        {
            return await _repository.IsExistBdAsync(nome.Trim(), senha);
        }

        public async Task<User> UpdateAsync(User user)
        {
            return await _repository.UpdateAsync(user);
        }

        public async Task<bool> RolesADExistsAsync()
        {
            return await _repository.RolesADExistsAsync();

        }

        public Task<bool> UserModelExistsAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
