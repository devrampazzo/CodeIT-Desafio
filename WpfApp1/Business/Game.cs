using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;
using WpfApp1.Model.Enum;

namespace WpfApp1.Business
{
    /// <summary>
    /// Controla as regras e mecânicas do game, exceto modificações em tela.
    /// </summary>
    public sealed class Game
    {
        private RepositorioPessoa repositorioPessoa;

        public Terminal Terminal { get; set; }
        public Aviao Aviao { get; set; }
        public SmartFortwo SmartFortwo { get; set; }
        public int NumeroViagens { get; set; }
        public bool JogoEncerrado { get; set; }
        public byte Segundos { get; set; }
        public byte Minutos { get; set; }

        static Game _instancia;

        public static Game Instancia
        {
            get => _instancia ?? (_instancia = new Game());
        }

        // Padrão Singleton para facilitar
        private Game()
        {
            repositorioPessoa = new RepositorioPessoa();
        }

        public void IniciarNovoJogo()
        {
            Terminal= new Terminal();
            Terminal.AdicionarVarias(repositorioPessoa.GetAll());

            Aviao = new Aviao();

            SmartFortwo = new SmartFortwo();

            NumeroViagens = 0;

            JogoEncerrado = false;

            Segundos = 0;
            Minutos = 0;
        }

        public void AtribuirMotorista(Pessoa pessoa)
        {
            if (pessoa.Piloto)
            {
                if (SmartFortwo.Motorista != null)
                    throw new RegraNaoAtendidaException("Já existe um motorista no veículo! Retornar o motorista para continuar.");
                if (Terminal.ObterPessoas().Any(p => p == pessoa))
                    Terminal.Remover(pessoa);
                if (Aviao.ObterPessoas().Any(p => p == pessoa))
                    Aviao.Remover(pessoa);

                SmartFortwo.Motorista = pessoa;
            }
            else
                throw new RegraNaoAtendidaException("A pessoa selecionada não pode dirigir o veículo!");
        }

        public void AtribuirPassageiro(Pessoa pessoa)
        {
            if (SmartFortwo.Motorista == null)
                throw new RegraNaoAtendidaException("Selecione primeiro o motorista!");

            if (pessoa is PessoaTransportePrisional passageiroTransportePrisional)
            {
                if (passageiroTransportePrisional.CoDependente != SmartFortwo.Motorista)
                    throw new RegraNaoAtendidaException($"O(a) {passageiroTransportePrisional.Nome} não pode viajar desacompanhado do(a) {passageiroTransportePrisional.CoDependente.Nome}!");
            }
            else
            {
                if (SmartFortwo.Motorista is PessoaTransportePrisional motoristaTransportePrisional)
                    throw new RegraNaoAtendidaException($"O(a) {motoristaTransportePrisional.Nome} não pode viajar desacompanhado do(a) {motoristaTransportePrisional.CoDependente.Nome}!");
            }

            if (pessoa is PessoaTripulacao pessoaTripulacao)
            {
                if (pessoaTripulacao.Grupo != (SmartFortwo.Motorista as PessoaTripulacao).Grupo && !pessoaTripulacao.Piloto)
                    throw new RegraNaoAtendidaException($"O(a) {pessoaTripulacao.Nome} não pode viajar com o(a) {SmartFortwo.Motorista.Nome}!");
            }

            if (Terminal.ObterPessoas().Any(p => p == pessoa))
                Terminal.Remover(pessoa);
            if (Aviao.ObterPessoas().Any(p => p == pessoa))
                Aviao.Remover(pessoa);

            SmartFortwo.Passageiro = pessoa;
        }

        public void CancelarViagem()
        {
            ILocal local;
            if (SmartFortwo.Local == LocaisEnum.Terminal)
                local = Terminal;
            else
                local = Aviao;

            if (SmartFortwo.Motorista != null)
            {
                local.Adicionar(SmartFortwo.Motorista);
                SmartFortwo.Motorista = null;
            }
            if (SmartFortwo.Passageiro != null)
            {
                local.Adicionar(SmartFortwo.Passageiro);
                SmartFortwo.Passageiro = null;
            }
        }

        public void DirigirPara()
        {
            if (SmartFortwo.Motorista == null)
                throw new RegraNaoAtendidaException("Nenhum motorista foi informado!");

            if (SmartFortwo.Passageiro == null && SmartFortwo.Motorista is PessoaTransportePrisional pessoaTransportePrisional)
                throw new RegraNaoAtendidaException($"O(a) {SmartFortwo.Motorista.Nome} não pode viajar desacompanhado do(a) {pessoaTransportePrisional.CoDependente.Nome}!");

            ILocal localDestino;
            if (SmartFortwo.Local == LocaisEnum.Terminal)
                localDestino = Aviao;
            else
                localDestino = Terminal;

            localDestino.Adicionar(SmartFortwo.Motorista);

            if (SmartFortwo.Passageiro != null)
                localDestino.Adicionar(SmartFortwo.Passageiro);

            SmartFortwo.Motorista = null;
            SmartFortwo.Passageiro = null;
            SmartFortwo.Local = SmartFortwo.Local == LocaisEnum.Terminal ? LocaisEnum.Aviao : LocaisEnum.Terminal;

            VerificaTerminoGame();
        }

        public void VerificaTerminoGame()
        {
            if (Terminal.ObterPessoas().Count == 0 && SmartFortwo.Motorista == null && SmartFortwo.Passageiro == null)
            {
                JogoEncerrado = true;
            }
        }

        public void TickClock()
        {
            if (Segundos++ == 60)
            {
                Minutos++;
                Segundos = 0;
            }
        }
    }
}
