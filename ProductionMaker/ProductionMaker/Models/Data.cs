using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionMaker.Models
{
    public class Data
    {
        private string _campo;
        private string _tipo;
        private string _titulo;
        private double _tamanho;

        public string Campo { get { return _campo; } set { _campo = value; } }
        public string Tipo { get { return _tipo; } set { _tipo = value; } }
        public string Titulo { get { return _titulo; } set { _titulo = value; } }
        public double Tamanho { get { return _tamanho; } set { _tamanho = value; } }
    }
}
