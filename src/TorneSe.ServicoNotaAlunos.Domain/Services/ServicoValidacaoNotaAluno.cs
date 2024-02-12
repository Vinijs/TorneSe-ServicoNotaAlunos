
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.Interfaces.Services;
using TorneSe.ServicoNotaAlunos.Domain.Entidades;

namespace TorneSe.ServicoNotaAlunos.Domain.Services;
public class ServicoValidacaoNotaAluno : IServicoValidacaoNotaAluno
{
    private void ValidarProfessor(Professor professor)
    {
        throw new NotImplementedException();
    }

    private void ValidarDisciplina(Disciplina disciplina)
    {
        throw new NotImplementedException();
    }

    private void ValidarAluno(Aluno aluno)
    {
        throw new NotImplementedException();
    }
    public void ValidarLancamento(Aluno aluno, Professor professor, Disciplina disciplina)
    {
        ValidarDisciplina(disciplina);
        ValidarProfessor(professor);
        ValidarAluno(aluno);
    }

}