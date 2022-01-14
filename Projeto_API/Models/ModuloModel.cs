using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_API.Models
{
    public class ModuloModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int QtdAulas { get; set; }

        public List<AulaModel> Aulas { get; set; }
    }
}
