using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductionMaker
{
    public class EnderFiles
    {
        public string Endereco { get; set; }
        public string Nome { get; set; }

        public EnderFiles(string Endereco,string Nome)
        {
            this.Endereco = Endereco;
            this.Nome = Nome;
        }
    }
}
