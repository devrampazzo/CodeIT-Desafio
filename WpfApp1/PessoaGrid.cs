using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfApp1.Model;

namespace CodeITAirlines
{
    /// <summary>
    /// Classe que herda o componente WPF Grid, monta um componente visual com borda, imagem, legenda e tooltip, além de permitir
    /// vinculação a um objeto Pessoa.
    /// </summary>
    public class PessoaGrid : Grid
    {
        protected Pessoa pessoaModel;

        public PessoaGrid(Pessoa pessoa) : base()
        {
            pessoaModel = pessoa;
            stackPanel = new StackPanel();
            stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            stackPanel.VerticalAlignment = VerticalAlignment.Center;

            iconePessoa = new Image();
            iconePessoa.Width = 68;
            iconePessoa.Height = 68;
            iconePessoa.Source = new BitmapImage(new Uri(pessoaModel.UriImagem, UriKind.Relative));

            if (pessoaModel != null && pessoaModel is PessoaTripulacao pessoaTripulacaoModel)
            {
                var tbLegendaGrupo = new TextBlock();
                tbLegendaGrupo.FontWeight = FontWeights.Bold;
                tbLegendaGrupo.Text = "Grupo:";
                tbGrupo = new TextBlock();
                tbGrupo.Text = pessoaTripulacaoModel.Grupo.Descricao;
                var spToolTip = new StackPanel();
                spToolTip.Children.Add(tbLegendaGrupo);
                spToolTip.Children.Add(tbGrupo);
                iconePessoa.ToolTip = spToolTip;
            }
            
            nomePessoa = new TextBlock();
            nomePessoa.Text = pessoaModel?.Nome;
            nomePessoa.FontSize = 12;
            nomePessoa.HorizontalAlignment = HorizontalAlignment.Center;
            nomePessoa.TextAlignment = TextAlignment.Center;
            nomePessoa.TextWrapping = TextWrapping.WrapWithOverflow;

            stackPanel.Children.Add(iconePessoa);
            stackPanel.Children.Add(nomePessoa);

            border = new Border();
            border.BorderThickness = new Thickness(2);
            border.BorderBrush = null;
            border.Width = 82;
            border.Height = 105;
            border.Child = stackPanel;

            this.Children.Add(border);
        }

        protected StackPanel stackPanel;
        protected Border border;
        protected Image iconePessoa;
        protected TextBlock nomePessoa;
        protected SolidColorBrush bordaSelecionada = Brushes.Purple;
        protected TextBlock tbGrupo;

        public bool Selecionada
        {
            get => border.BorderBrush == bordaSelecionada;

            set
            {
                if (value)
                    border.BorderBrush = bordaSelecionada;
                else
                    border.BorderBrush = null;

                pessoaModel.Selecionada = value;
            }
        }

    }
}
