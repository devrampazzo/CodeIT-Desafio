namespace WpfApp1.Model
{
    /// <summary>
    /// Derivação de Pessoa, permite associar a um Grupo.
    /// </summary>
    public class PessoaTripulacao : Pessoa
    {
        public Grupo Grupo { get; set; }
    }
}
