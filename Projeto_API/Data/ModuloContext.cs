using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_API.Data
{
    public class ModuloContext : DbContext
    {
        public ModuloContext(DbContextOptions<ModuloContext> options)
            : base(options)
        {
        }

        public DbSet<Models.ModuloModel> Modulos { get; set; }
        public DbSet<Models.AulaModel> Aulas { get; set; }
        public DbSet<Models.User> Users { get; set; }

        internal object Entry<T>()
        {
            throw new NotImplementedException();
        }
    }
}