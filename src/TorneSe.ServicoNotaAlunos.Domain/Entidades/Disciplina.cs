using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.Enums;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;

namespace TorneSe.ServicoNotaAlunos.Domain.Entidades;
public class Disciplina : Entidade, IRaizAgregacao
{
    public Disciplina(string nome,string descricao, DateTime dataInicio,
                      DateTime dataFim, TipoDisciplina tipoDisciplina,DateTime dataCadastro, int professorId)
    {
        Nome = nome;
        Descricao = descricao;
        DataInicio = dataInicio;
        DataFim = dataFim;
        TipoDisciplina = tipoDisciplina;
        DataCadastro = dataCadastro;
        ProfessorId = professorId;
        Conteudos = new List<Conteudo>();
    }

    protected Disciplina(){ }

    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public DateTime DataInicio { get; private set; }
    public DateTime DataFim { get; private set; }
    public TipoDisciplina TipoDisciplina { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public int ProfessorId { get; private set; }

    public Professor Professor { get; private set; }
    public ICollection<Conteudo> Conteudos { get; private set; }
}