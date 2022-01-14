using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_API.Models
{
    public class AulaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataAula { get; set; }

        public int ModuloId { get; set; }
        public ModuloModel Modulo { get; set; }
    }
}
