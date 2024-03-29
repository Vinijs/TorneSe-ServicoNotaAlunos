using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;
using TorneSe.ServicoNotaAlunos.Domain.Entidades;
using TorneSe.ServicoNotaAlunos.Data.Environment;

namespace TorneSe.ServicoNotaAlunos.Data.Context;

public class ServicoNotaAlunosContexto : DbContext
{
    public ServicoNotaAlunosContexto(DbContextOptions<ServicoNotaAlunosContexto> options) 
        : base(options){}

    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<AlunosTurmas> AlunosTurmas { get; set; }
    public DbSet<Atividade> Atividades { get; set; }
    public DbSet<Conteudo> Conteudos { get; set; }
    public DbSet<Disciplina> Disciplinas { get; set; }
    public DbSet<Nota> Notas { get; set; }
    public DbSet<Professor> Professores { get; set; }
    public DbSet<Turma> Turmas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            optionsBuilder
            .UseNpgsql(ProvedorVariaveisAmbiente.Instancia.DefaultConnection);
        }

        // optionsBuilder.LogTo(Console.WriteLine);
        // optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // var assembly = Assembly.GetExecutingAssembly();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ServicoNotaAlunosContexto).Assembly);
        base.OnModelCreating(modelBuilder);
    }

     public override void Dispose()
    {
        base.Dispose();
    }

    public bool ContextoPossuiMudancas() =>
         ChangeTracker.HasChanges();
}