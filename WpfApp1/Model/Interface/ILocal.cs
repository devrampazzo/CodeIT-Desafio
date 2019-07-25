using System.Collections.Generic;

namespace WpfApp1.Model
{
    /// <summary>
    /// Define um contrato comum para os locais, controlando o rol de pessoas no local.
    /// As classes que criei para implementar esta interface acabaram ficando iguais, poderia apenas ter feito uma classe Local, mas 
    /// deixei a interface apenas para implementar o uso.
    /// </summary>
    public interface ILocal
    {
        List<Pessoa> ObterPessoas();
        Pessoa ObterSelecionada();
        void Remover(Pessoa pessoa);
        void Adicionar(Pessoa pessoa);
        void EsvaziarLocal();
        void AdicionarVarias(List<Pessoa> pessoas);
    }
}
