using Microsoft.EntityFrameworkCore;
using Projeto_API.Interface.Repositorio;
using Projeto_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Projeto_API.Data.Repositorio
{
    public class AulaRepositorio : IAulaRepositorio
    {
        private readonly ModuloContext _context;

        public AulaRepositorio(ModuloContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int id)
        {
            var aula = await GetByIdAsync(id);

            var removeAula = _context.Aulas.Remove(aula);

            await _context.SaveChangesAsync();

            _context.Entry(removeAula.Entity).State = EntityState.Detached;

            UpdateQtdAulas(aula.ModuloId);
        }

        public async Task<IEnumerable<AulaModel>> GetAllAsync()
        {
            return await _context.Aulas
                            .OrderBy(x => x.Nome)
                            .Include(p => p.Modulo)
                            .ToListAsync();
        }

        public async Task<AulaModel> CreateAsync(AulaModel aula)
        {

            var newAula = _context.Aulas.Add(aula);
            await _context.SaveChangesAsync();

            _context.Entry(newAula.Entity).State = EntityState.Detached;

            UpdateQtdAulas(aula.ModuloId);

            return newAula.Entity;
        }

        public async Task<AulaModel> GetByIdAsync(int id)
        {
            return await _context.Aulas
                        .Include(p => p.Modulo)
                        .OrderBy(x => x.Nome)
                        .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<AulaModel> UpdateAsync(AulaModel aula)
        {
            var editAluno = _context.Aulas.Update(aula);
            await _context.SaveChangesAsync();

            _context.Entry(editAluno.Entity).State = EntityState.Detached;

            UpdateQtdAulas(aula.ModuloId);

            return editAluno.Entity;
        }

        public async Task<bool> IsExistBdAsync(string nome, int id)
        {
            var s = await _context.Aulas
                          .FirstOrDefaultAsync(x => x.Nome == nome && x.Id != id);

            return s != null;
        }

        private void UpdateQtdAulas(int id)
        {

            var newMod = _context.Modulos
                         .Include(p => p.Aulas)
                         .DeferredFirstOrDefault(x => x.Id == id)
                         .FutureValue();

            var qtdAulasFuture = _context.Aulas
                          .DeferredCount(x => x.ModuloId == id)
                          .FutureValue();

            if (newMod != null && (newMod.Value.QtdAulas!= qtdAulasFuture.Value))
            {
                newMod.Value.QtdAulas = qtdAulasFuture.Value;
                _context.Modulos.Update(newMod);
                _context.SaveChanges();
            }
        }
    }
}
