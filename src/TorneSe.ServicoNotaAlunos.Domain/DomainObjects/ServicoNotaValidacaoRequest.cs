using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.Entidades;

namespace TorneSe.ServicoNotaAlunos.Domain.DomainObjects;
public class ServicoNotaValidacaoRequest
{
    public static ServicoNotaValidacaoRequest Instance => new ServicoNotaValidacaoRequest();
    public int AlunoId { get; set; }
    public Aluno Aluno {get; set;}
    public int ProfessorId { get; set; }
    public Professor Professor {get; set;}
    public int AtividadeId { get; set; }
    public Disciplina Disciplina { get; set; }

}
