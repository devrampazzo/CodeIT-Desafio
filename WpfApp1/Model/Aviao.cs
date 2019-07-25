using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class Aviao : ILocal
    {
        private readonly List<Pessoa> listaPessoas;

        public Aviao()
        {
            listaPessoas = new List<Pessoa>();
        }

        public void Adicionar(Pessoa pessoa)
        {
            listaPessoas?.Add(pessoa);
        }

        public Pessoa ObterSelecionada() => listaPessoas?.FirstOrDefault(p => p.Selecionada);

        public void Remover(Pessoa pessoa)
        {
            listaPessoas?.Remove(pessoa);
        }

        public void EsvaziarLocal()
        {
            listaPessoas?.RemoveAll(p => true);
        }

        public void AdicionarVarias(List<Pessoa> pessoas)
        {
            listaPessoas?.AddRange(pessoas);
        }

        public List<Pessoa> ObterPessoas() => listaPessoas;
    }
}
