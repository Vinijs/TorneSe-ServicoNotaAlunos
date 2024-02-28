using TorneSe.ServicoNotaAlunos.Domain.Entidades;

namespace TorneSe.ServicoNotaAlunos.UnitTests.Fakes;
public static class ProfessorFakes
{
    public static Professor RetornaProfessorValido()
        => new(1282727, "Danilo",
        "danilo.s@email.com", true, false, 1212, DateTime.Now, 1341567);

    public static Professor RetornaProfessorInativo()
    {
        var professor = new Professor(1282727, "Danilo",
        "danilo.s@email.com", true, false, 1212, DateTime.Now, 1341567);

        professor.TornarInativo();

        return professor;
    }
}