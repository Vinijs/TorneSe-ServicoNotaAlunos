using TorneSe.ServicoNotaAlunos.Domain.Entidades;
using TorneSe.ServicoNotaAlunos.Domain.Enums;

namespace TorneSe.ServicoNotaAlunos.UnitTests.Fakes;
public static class AlunoFakes
{
    public static Aluno RetornaAluno()
    {
        var aluno = new Aluno(1234, "Raphael", "raphael.s@email.com", 1212, DateTime.Now);

        aluno.AlunosTurmas = new List<AlunosTurmas>()
        {
            new(1234,10019,DateTime.Now)
            {
                Turma =
                    new("Grupo Matemática I", Periodo.Noturno, new DateTime(2021,06,01),
                    new DateTime(2021,12,01), DateTime.Now, 1341567)
            }
        };

        return aluno;
    }

    public static Aluno RetornaAlunoInativo()
    {
        var aluno = new Aluno(1234, "Raphael", "raphael.s@email.com", 1212, DateTime.Now);
        aluno.TornarInativo();
        return aluno;
    }
}