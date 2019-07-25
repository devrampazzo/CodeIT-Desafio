namespace WpfApp1.Model
{
    /// <summary>
    /// Derivação de Pessoa, permite associar a pessoa a outra pessoa, criando uma relação de co-dependência.
    /// </summary>
    public class PessoaTransportePrisional : Pessoa
    {
        public PessoaTransportePrisional CoDependente { get; set; }
    }
}
