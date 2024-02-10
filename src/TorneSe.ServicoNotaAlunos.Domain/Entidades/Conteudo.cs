using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneSe.ServicoNotaAlunos.Domain.Entidades;

    public class Conteudo: Entidade
    {
        public Conteudo(string nome,string descricao, DateTime dataInicio,
                        DateTime dataTermino, DateTime dataCadastro) 
        {
            Nome = nome;
            Descricao = descricao;
            DataInicio = dataInicio;
            DataTermino = dataTermino;
            DataCadastro = dataCadastro;
            Atividades = new List<Atividade>();
               
        }

        protected Conteudo() { }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataTermino { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public Disciplina Disciplina { get; private set; }
        public ICollection<Atividade> Atividades { get; private set; }

        public void CadastrarAtividade(Atividade atividade) =>
            Atividades.Add(atividade);


    }
