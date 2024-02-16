using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TorneSe.ServicoNotaAlunos.Domain.Entidades;

namespace TorneSe.ServicoNotaAlunos.Data.Mappings;
public class TurmaMapping : IEntityTypeConfiguration<Turma>
{
    public void Configure(EntityTypeBuilder<Turma> builder)
    {
        builder.ToTable("turmas", "servnota")
               .HasKey(x => x.Id);

        builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityAlwaysColumn<int>()
                .HasIdentityOptions<int>(1, 1);

        builder.Property(x => x.Nome)
               .HasColumnName("nome")
               .HasColumnType("varchar(50)")
               .IsRequired();

        builder.Property(x => x.Periodo)
               .HasColumnName("periodo")
               .HasConversion<string>()
               .IsRequired();

        builder.Property(x => x.DataCadastro)
               .HasColumnName("data_cadastro")
               .HasColumnType("timestamp(6)")
               .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.DataInicio)
               .HasColumnName("data_inicio")
               .HasColumnType("timestamp(6)")
               .IsRequired();

        builder.Property(x => x.DataFinal)
               .HasColumnName("data_final")
               .HasColumnType("timestamp(6)")
               .IsRequired();

        builder.Property(x => x.DisciplinaId)
               .HasColumnName("disciplina_id")
               .HasColumnType("int")
               .IsRequired();

        builder.HasOne(x => x.Disciplina)
               .WithMany(x => x.Turmas)
               .HasForeignKey(x => x.DisciplinaId)
               .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasMany(x => x.Alunos)
               .WithMany(x => x.Turmas)
               .UsingEntity<AlunosTurmas>("alunos_turmas",
               x => x.HasOne(y => y.Aluno)
                      .WithMany(x => x.AlunosTurmas)
                      .HasForeignKey(x => x.AlunoId),
                      x => x.HasOne(y => y.Turma)
                      .WithMany(x => x.AlunosTurmas)
                      .HasForeignKey(x => x.TurmaId),
                      x =>
                      {
                          x.ToTable("alunos_turmas")
                                  .HasKey(y => new { y.AlunoId, y.TurmaId });

                          x.Property(x => x.AlunoId)
                                  .HasColumnName("aluno_id")
                                  .HasColumnType("INT")
                                  .IsRequired();

                          x.Property(x => x.TurmaId)
                                  .HasColumnName("turma_id")
                                  .HasColumnType("INT")
                                  .IsRequired();

                          x.Property(x => x.DataCadastro)
                                  .HasColumnName("data_cadastro")
                                  .HasColumnType("TIMESTAMP(6)")
                                  .HasDefaultValueSql("CURRENT_TIMESTAMP");
                      });
    }
}