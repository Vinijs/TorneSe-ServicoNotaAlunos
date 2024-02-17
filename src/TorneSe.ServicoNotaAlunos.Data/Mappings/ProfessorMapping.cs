using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TorneSe.ServicoNotaAlunos.Domain.Entidades;

namespace TorneSe.ServicoNotaAlunos.Data.Mappings;
public class ProfessorMapping : IEntityTypeConfiguration<Professor>
{
    public void Configure(EntityTypeBuilder<Professor> builder)
    {
        builder.ToTable("professores", "servnota");

        builder.Property(x => x.NomeAbreviado)
                .HasColumnName("nome_abreviado")
                .HasMaxLength(50)
                .HasColumnType("VARCHAR(50)")
                .IsRequired();

        builder.Property(x => x.EmailInterno)
                .HasColumnName("email_interno")
                .HasMaxLength(100)
                .HasColumnType("VARCHAR(100)")
                .IsRequired();

        builder.Property(x => x.ProfessorTitular)
                .HasColumnName("professor_titular")
                .HasColumnType("BOOLEAN")
                .HasDefaultValueSql("TRUE")
                .IsRequired();

        builder.Property(x => x.ProfessorSuplente)
                .HasColumnName("professor_suplente")
                .HasColumnType("BOOLEAN")
                .HasDefaultValueSql("FALSE")
                .IsRequired();

        builder.Property(x => x.DataCadastro)
                .HasColumnName("data_cadastro")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.DisciplinaId)
                .HasColumnName("diciplina_id")
                .HasColumnType("INT")
                .IsRequired();

        builder.HasOne(x => x.Disciplina)
                .WithOne(x => x.Professor)
                .HasForeignKey<Professor>(x => x.DisciplinaId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        builder.Navigation(x => x.Disciplina)
                .AutoInclude();
    }
}