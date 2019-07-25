using WpfApp1.Model.Enum;

namespace WpfApp1.Model
{
    public class SmartFortwo
    {
        public Pessoa Motorista { get; set; }
        public Pessoa Passageiro { get; set; }
        public LocaisEnum Local { get; set; }

        public SmartFortwo()
        {
            ResetarVeiculo();
        }

        public void ResetarVeiculo()
        {
            Motorista = null;
            Passageiro = null;
            Local = LocaisEnum.Terminal;
        }
    }
}
