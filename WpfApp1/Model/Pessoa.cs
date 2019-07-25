using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class Pessoa
    {
        public Pessoa()
        {
            Selecionada = false;
        }

        public string Nome { get; set; }
        public string UriImagem { get; set; }
        public bool Selecionada { get; set; }
        public bool Piloto { get; set; }
    }
}
