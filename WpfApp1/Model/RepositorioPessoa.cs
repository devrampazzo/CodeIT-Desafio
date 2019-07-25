using System.Collections.Generic;

namespace WpfApp1.Model
{
    /// <summary>
    /// Encapsula o acesso / obtenção das pessoas envolvidas no game.
    /// </summary>
    public class RepositorioPessoa
    {
        public List<Pessoa> GetAll()
        {
            var listaPessoas = new List<Pessoa>();

            // LISTA DE PESSOAS HARD-CODED
            var grupoTecnico = new Grupo
            {
                TipoGrupo = GruposEnum.TripulacaoTecnica,
                Descricao = "Tripulação Técnica"
            };
            var grupoCabine = new Grupo
            {
                TipoGrupo = GruposEnum.TripulacaoCabine,
                Descricao = "Tripulação de Cabine"
            };

            listaPessoas.Add(
                new PessoaTripulacao
                {
                    Grupo = grupoTecnico,
                    Nome = "Piloto",
                    Piloto = true,
                    UriImagem = "img/pilot.png"
                });
            listaPessoas.Add(
                new PessoaTripulacao
                {
                    Grupo = grupoTecnico,
                    Nome = "Oficial 1",
                    Piloto = false,
                    UriImagem = "img/officer.png"
                });
            listaPessoas.Add(
                new PessoaTripulacao
                {
                    Grupo = grupoTecnico,
                    Nome = "Oficial 2",
                    Piloto = false,
                    UriImagem = "img/officer.png"
                });
            listaPessoas.Add(
                new PessoaTripulacao
                {
                    Grupo = grupoCabine,
                    Nome = "Chefe Serviço",
                    Piloto = true,
                    UriImagem = "img/flight-attendant.png"
                });
            listaPessoas.Add(
                new PessoaTripulacao
                {
                    Grupo = grupoCabine,
                    Nome = "Comissária 1",
                    Piloto = false,
                    UriImagem = "img/stewardess.png"
                });
            listaPessoas.Add(
                new PessoaTripulacao
                {
                    Grupo = grupoCabine,
                    Nome = "Comissária 2",
                    Piloto = false,
                    UriImagem = "img/stewardess.png"
                });

            var policial = new PessoaTransportePrisional
                {
                    Nome = "Policial",
                    Piloto = true,
                    UriImagem = "img/police.png"
            };
            var presidiario = new PessoaTransportePrisional
            {
                CoDependente = policial,
                Nome = "Presidiário",
                Piloto = false,
                UriImagem = "img/prisoner.png"
            };

            policial.CoDependente = presidiario;

            listaPessoas.Add(policial);
            listaPessoas.Add(presidiario);

            return listaPessoas;
        }
    }
}
