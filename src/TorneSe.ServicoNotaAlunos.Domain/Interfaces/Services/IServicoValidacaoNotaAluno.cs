using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.Entidades;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;

namespace TorneSe.ServicoNotaAlunos.Domain.Interfaces.Services;
public interface IServicoValidacaoNotaAluno
{
    void ValidarLancamento(Aluno aluno, Professor professor, Disciplina disciplina);
    void ValidarLancamento(ServicoNotaValidacaoRequest request);

}
