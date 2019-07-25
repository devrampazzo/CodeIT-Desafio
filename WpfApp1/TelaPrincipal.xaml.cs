using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using WpfApp1.Business;
using WpfApp1.Model;
using WpfApp1.Model.Enum;

namespace CodeITAirlines
{
    /// <summary>
    /// Interação lógica para TelaPrincipal.xam
    /// </summary>
    public partial class TelaPrincipal : Window
    {
        DispatcherTimer cronometro;

        public TelaPrincipal()
        {
            InitializeComponent();
            cronometro = new DispatcherTimer();
            cronometro.Tick += Cronometro_Tick;
            cronometro.Interval = new TimeSpan(0, 0, 1);
            tbEscolherMotoristaTerminal.MouseDown += (sender, e) => RenderizarTela();
            tbEscolherMotoristaAviao.MouseDown += (sender, e) => RenderizarTela();
            tbEscolherPassageiroTerminal.MouseDown += (sender, e) => RenderizarTela();
            tbEscolherPassageiroAviao.MouseDown += (sender, e) => RenderizarTela();
            btnIniciar.Click += (sender, e) => RenderizarTela();
            tbCancelarViagem.MouseDown += (sender, e) => RenderizarTela();
            tbDirigirPara.MouseDown += (sender, e) => RenderizarTela();
        }

        private void Cronometro_Tick(object sender, EventArgs e)
        {
            Game.Instancia.TickClock();
            AtualizarClock();
        }

        protected void AtualizarClock()
        {
            lblClock.Content = $"{string.Format("{0:D2}",Game.Instancia.Minutos)}:{string.Format("{0:D2}", Game.Instancia.Segundos)}";
        }

        private void WpTerminal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelecionarPessoa(e.Source, sender as WrapPanel);
        }

        /// <summary>
        /// Atualiza visual e logicamente a pessoa marcada como 'selecionada'.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="panelOrigem"></param>
        protected void SelecionarPessoa(object source, WrapPanel panelOrigem)
        {
            foreach (object grid in panelOrigem.Children)
            {
                if (grid is PessoaGrid pGrid)
                    pGrid.Selecionada = false;
            }

            if (source is Image img && img.Parent is StackPanel sPanel && sPanel.Parent is Border borda && borda.Parent is PessoaGrid pessoaGrid)
                pessoaGrid.Selecionada = true;
        }

        private void BtnIniciar_Click(object sender, RoutedEventArgs e)
        {
            Game.Instancia.IniciarNovoJogo();
            cronometro.Start();
        }

        /// <summary>
        /// Atualiza a tela de acordo com o estado atual.
        /// </summary>
        protected void RenderizarTela()
        {
            var gameInstancia = Game.Instancia;

            spFortwo.IsEnabled = true;

            if (gameInstancia.SmartFortwo.Local == LocaisEnum.Terminal)
            {
                spAviao.IsEnabled = false;
                spTerminal.IsEnabled = true;
            }

            if (gameInstancia.SmartFortwo.Local == LocaisEnum.Aviao)
            {
                spAviao.IsEnabled = true;
                spTerminal.IsEnabled = false;
            }

            wpTerminal.Children.Clear();
            wpAviao.Children.Clear();
            grdMotorista.Children.Clear();
            grdPassageiro.Children.Clear();

            var listaTerminal = gameInstancia.Terminal.ObterPessoas();

            foreach (var pessoa in listaTerminal)
            {
                var pessoaGrid = new PessoaGrid(pessoa);
                wpTerminal.Children.Add(pessoaGrid);
            }

            var listaAviao = gameInstancia.Aviao.ObterPessoas();

            foreach (var pessoa in listaAviao)
            {
                var pessoaGrid = new PessoaGrid(pessoa);
                wpAviao.Children.Add(pessoaGrid);
            }

            var fortwo = gameInstancia.SmartFortwo;

            if (fortwo.Local == LocaisEnum.Aviao)
            {
                var scale = new ScaleTransform(1, 1);
                imgFortwo.RenderTransform = scale;
            } 
            else
            {
                var scale = new ScaleTransform(-1, 1);
                imgFortwo.RenderTransform = scale;
            }

            if (fortwo.Motorista != null)
                grdMotorista.Children.Add(new PessoaGrid(fortwo.Motorista));
            if (fortwo.Passageiro != null)
                grdPassageiro.Children.Add(new PessoaGrid(fortwo.Passageiro));

            tbDirigirPara.Text = gameInstancia.SmartFortwo.Local == LocaisEnum.Terminal ? "Dirigir para avião >>" : "<< Dirigir para Terminal";
            lblNumeroViagens.Content = gameInstancia.NumeroViagens.ToString();

            if (gameInstancia.JogoEncerrado)
            {
                spTerminal.IsEnabled = false;
                spAviao.IsEnabled = false;
                spFortwo.IsEnabled = false;
            }
        }

        private void TbEscolherMotoristaTerminal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AtribuirMotorista(Game.Instancia.Terminal);
        }

        private void TbEscolherMotoristaAviao_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AtribuirMotorista(Game.Instancia.Aviao);
        }

        /// <summary>
        /// Atribui a pessoa selecionada como motorista do veículo.
        /// </summary>
        /// <param name="local">Define em qual local está a pessoa selecionada.</param>
        protected void AtribuirMotorista(ILocal local)
        {
            var pessoaSelecionada = local.ObterSelecionada();

            if (pessoaSelecionada == null)
            {
                MessageBox.Show("Selecione uma pessoa!");
                return;
            }

            try
            {
                Game.Instancia.AtribuirMotorista(pessoaSelecionada);
            }
            catch (RegraNaoAtendidaException rex)
            {
                MessageBox.Show(rex.Message);
            }
            catch
            {
                throw;
            }
        }

        private void TbEscolherPassageiroTerminal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AtribuirPassageiro(Game.Instancia.Terminal);
        }

        private void TbEscolherPassageiroAviao_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AtribuirPassageiro(Game.Instancia.Aviao);
        }

        /// <summary>
        /// Atribui a pessoa selecionada como passageiro do veículo.
        /// </summary>
        /// <param name="local">Define em qual local está a pessoa selecionada.</param>
        protected void AtribuirPassageiro(ILocal local)
        {
            var pessoaSelecionada = local.ObterSelecionada();

            if (pessoaSelecionada == null)
            {
                MessageBox.Show("Selecione uma pessoa!");
                return;
            }

            try
            {
                Game.Instancia.AtribuirPassageiro(pessoaSelecionada);
            }
            catch (RegraNaoAtendidaException rex)
            {
                MessageBox.Show(rex.Message);
            }
            catch
            {
                throw;
            }
        }

        private void TbCancelarViagem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Game.Instancia.CancelarViagem();
        }

        private void TbDirigirPara_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var gameInstancia = Game.Instancia;

            try
            {
                gameInstancia.DirigirPara();
                gameInstancia.NumeroViagens++;

                if (gameInstancia.JogoEncerrado)
                {
                    cronometro.Stop();
                    MessageBox.Show($"Parabéns! Desafio finalizado em {gameInstancia.NumeroViagens} viagens.");
                }       
            }
            catch (RegraNaoAtendidaException rex)
            {
                MessageBox.Show(rex.Message);
            }
            catch
            {
                throw;
            }
        }

        private void WpAviao_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelecionarPessoa(e.Source, sender as WrapPanel);
        }

    }
}
