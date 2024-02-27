using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.Messages;


namespace TorneSe.ServicoNotaAlunos.UnitTests.Fakes;
public static class RegistrarNotaAlunoFakes
{
    public static RegistrarNotaAluno RetornaMensagemValida() =>
        new()
        {
            AlunoId = 1235,
            AtividadeId = 2,
            CorrelationId = Guid.NewGuid(),
            ProfessorId = 1282727,
            ValorNota = 8,
            NotaSubstitutiva = true
        };

    public static RegistrarNotaAluno RetornaMensagemInvalida() =>
        new()
        {
            AlunoId = 0,
            AtividadeId = 0,
            CorrelationId = Guid.Empty,
            ProfessorId = 0,
            ValorNota = -1,
            NotaSubstitutiva = false
        };
}