using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionMaker.Models
{
    public class Arquivo
    {
        private string _fullName = "";
        private string _name = "";
        private DateTime _data;
        private string _encoding = "";
        private bool _ok = true;
        private long _tamanho = 0;

        public string FullName { get { return _fullName; } set { _fullName = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public DateTime Data { get { return _data; } set { _data = value; } }
        public string Encoding { get { return _encoding; } set { _encoding = value; } }
        public bool OK { get { return _ok; } set { _ok = value; } }
        public long Tamanho { get { return _tamanho; } set { _tamanho = value; } }
    }
}
