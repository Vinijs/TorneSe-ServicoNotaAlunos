using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.Entidades;
using TorneSe.ServicoNotaAlunos.Domain.Enums;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;


namespace TorneSe.ServicoNotaAlunos.Data.Context;
public class FakeDbContexto : IDisposable, IUnitOfWork
{
    public FakeDbContexto()
    {
        Alunos = AlunosFake();
        Professores = ProfessoresFake();
        Disciplinas = DisciplinasFake();
    }
    public ICollection<Aluno> Alunos { get; set; }
    public ICollection<Professor> Professores { get; set; }
    public ICollection<Disciplina> Disciplinas { get; set; }

    private ICollection<Aluno> AlunosFake()
    {
        var alunos = new List<Aluno>();

        Aluno aluno = new(1234, "Raphael", "raphael@gmail.com", 1212, DateTime.Now);

        alunos.Add(aluno);

        return alunos;
    }

    private ICollection<Professor> ProfessoresFake()
    {
        var professores = new List<Professor>();

        Professor professor = new(1282727, "Danilo", "danilo.s@gmail.com", true, false, 1212, DateTime.Now);

        professores.Add(professor);

        return professores; 
    }

     private ICollection<Disciplina> DisciplinasFake()
    {
        //biblioteca Bogus para gerar dados falsos é uma oportunidade

        var disciplinas = new List<Disciplina>();

        var disciplina = new Disciplina("Matemática", "Matemática base ensino médio"
        , new DateTime(2021,10,12),new DateTime(2022,02,12), TipoDisciplina.Teorica, 
        new DateTime(2021, 09,12), 1282727);

        var conteudo = new Conteudo("Equações segundo grau", "Aprendizado de equações de segundo grau",
        new DateTime(2021,10,18), new DateTime(2021,11,18), new DateTime(2021,10,15));

        var atividade = new Atividade(34545,"Atividade avaliativa equações", TipoAtividade.Avaliativa,
         new DateTime(2021,11,10), new DateTime(2021, 11, 01), false);

        conteudo.CadastrarAtividade(atividade);

        disciplina.AdicionarConteudo(conteudo);

        disciplinas.Add(disciplina);

        return disciplinas;
    }

    public async Task<bool> Commit()
        => await Task.FromResult(true);

    public void Dispose()
    {
        Console.WriteLine("Fazendo liberação de recursos...");
    }


}