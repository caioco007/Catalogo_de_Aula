using Microsoft.EntityFrameworkCore;
using Projeto_API.Data;
using Projeto_API.Interface.Repositorio;
using Projeto_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Data.Repositorio
{
    public class ModuloRepositorio : IModuloRepositorio
    {
        private readonly ModuloContext _context;

        public ModuloRepositorio(ModuloContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int id)
        {
            var modulo = await _context.Modulos.FindAsync(id);

            _context.Modulos.Remove(modulo);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ModuloModel>> GetAllAsync()
        {

            var modulos =  _context.Modulos
                                .OrderBy(x => x.Nome)
                                .AsQueryable();

            var result = await modulos
                .Select(x => new
                {
                    Modulo = x,
                    QtdeAulas = x.Aulas.Count
                })
                .ToListAsync();


            var modulosResult = result
                .Select(x =>
                {
                    x.Modulo.QtdAulas = x.QtdeAulas;
                    return x.Modulo;
                });

            return modulosResult;

            //return await _context.Modulos
            //                .Include(p => p.Aulas)
            //                .OrderBy(x => x.Nome)
            //                .ToListAsync();
        }

        public async Task<ModuloModel> GetByIdAsync(int id)
        {
            var moduloFuture= _context.Modulos
                         .Include(p => p.Aulas)
                         .DeferredFirstOrDefault(x => x.Id == id)
                         .FutureValue();

            var qtdAulasFuture = _context.Aulas
                          .DeferredCount(x => x.ModuloId == id)
                          .FutureValue();

            var modulo = await moduloFuture.ValueAsync();

            modulo.QtdAulas = await qtdAulasFuture.ValueAsync();

            return modulo;

        }

        public async Task<ModuloModel> CreateAsync(ModuloModel modulo)
        {
            var newModulo = _context.Modulos.Add(modulo);
            await _context.SaveChangesAsync();

            return newModulo.Entity;
        }

        public async Task<ModuloModel> IsExistBdAsync(string nome, int id)
        {
            var modulo = await _context.Modulos
                    .FirstOrDefaultAsync(x => x.Nome.Contains(nome) && x.Id != id);

            return modulo;
        }

        public async Task<ModuloModel> UpdateAsync(ModuloModel modulo)
        {
            var editModulo = _context.Modulos.Update(modulo);
            await _context.SaveChangesAsync();

            return editModulo.Entity;
        }
    }
}
