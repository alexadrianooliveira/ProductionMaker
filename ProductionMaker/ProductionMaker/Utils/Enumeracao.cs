using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionMaker.Utils
{
    public class Enumeracao
    {
        public enum TipoDiretorio
        {
            GravarEntrada = 1,
            LerEntrada = 2,
            GravarSaida = 3,
            LerSaida = 4
        }

        public enum TipoRetorno
        {
            WsDataExterno = 1,
            WsDataInterno = 2,
            Select = 3,
            Insert = 4,
            Update = 5
        }
    }
}
