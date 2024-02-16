using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.Enums;

namespace TorneSe.ServicoNotaAlunos.Domain.Entidades;
    public class Turma : Entidade
{
    public Turma(string nome,Periodo periodo, DateTime dataInicio,
                 DateTime dataFinal, DateTime dataCadastro, int disciplinaId)
    {
        Nome = nome;
        Periodo = periodo;
        DataInicio = dataInicio;
        DataFinal = dataFinal;
        DataCadastro = dataCadastro;
        // DisciplinaId = disciplinaId;
        AlunosTurmas = new List<AlunosTurmas>();

    }

    protected Turma(){ }
    public string Nome { get; private set; }
    public Periodo Periodo { get; private set; }
    public DateTime DataInicio { get; private set; }
    public DateTime DataFinal { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public int DisciplinaId { get; private set; }
    public Disciplina Disciplina { get; private set; }
    public ICollection<AlunosTurmas> AlunosTurmas { get; set; }
    public ICollection<Aluno> Alunos { get; private set; }
}