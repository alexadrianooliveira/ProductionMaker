using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionMaker.Models
{
    public class PathFile
    {
        public string Endereco { get; set; }
        public string Nome { get; set; }

        public PathFile(string Endereco, string Nome)
        {
            this.Endereco = Endereco;
            this.Nome = Nome;
        }
    }
}
