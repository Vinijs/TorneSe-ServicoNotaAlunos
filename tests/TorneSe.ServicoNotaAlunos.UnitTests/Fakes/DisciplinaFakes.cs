using TorneSe.ServicoNotaAlunos.Domain.Entidades;
using TorneSe.ServicoNotaAlunos.Domain.Enums;

namespace TorneSe.ServicoNotaAlunos.UnitTests.Fakes;
public static class DisciplinaFakes
{
    public static Disciplina RetornaDisciplinaValida()
    {
          var disciplina = new Disciplina(1341567,"Matemática", "Matemática base ensino médio"
        , new DateTime(2023,10,12),new DateTime(2024,10,12), TipoDisciplina.Teorica, 
        new DateTime(2023, 09,12));

        var conteudo = new Conteudo("Equações segundo grau", "Aprendizado de equações de segundo grau",
        new DateTime(2023,10,18), new DateTime(2023,11,18), new DateTime(2023,10,15));

        var atividade = new Atividade(34545,"Atividade avaliativa equações", TipoAtividade.Avaliativa,
         new DateTime(2023,11,10), new DateTime(2023, 11, 01), false);

        conteudo.CadastrarAtividade(atividade);

        disciplina.AdicionarConteudo(conteudo);

        return disciplina;
    }

    public static Disciplina RetornaDisciplinaEncerrada()
    {
         var disciplina = new Disciplina(1341567,"Matemática", "Matemática base ensino médio"
        , new DateTime(2023,10,12),new DateTime(2024,02,12), TipoDisciplina.Teorica, 
        new DateTime(2021, 09,12));

        var conteudo = new Conteudo("Equações segundo grau", "Aprendizado de equações de segundo grau",
        new DateTime(2021,10,18), new DateTime(2021,11,18), new DateTime(2021,10,15));

        var atividade = new Atividade(34545,"Atividade avaliativa equações", TipoAtividade.Avaliativa,
         new DateTime(2021,11,10), new DateTime(2021, 11, 01), false);

        conteudo.CadastrarAtividade(atividade);

        disciplina.AdicionarConteudo(conteudo);

        return disciplina;
    }
}