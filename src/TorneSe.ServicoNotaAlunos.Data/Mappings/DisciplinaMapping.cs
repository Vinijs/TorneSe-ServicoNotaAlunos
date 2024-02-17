using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TorneSe.ServicoNotaAlunos.Domain.Entidades;

namespace TorneSe.ServicoNotaAlunos.Data.Mappings;

public class DisciplinaMapping : IEntityTypeConfiguration<Disciplina>
{
    public void Configure(EntityTypeBuilder<Disciplina> builder)
    {
         builder.ToTable("disciplinas", "servnota")
                .HasKey(x => x.Id);

        builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityAlwaysColumn<int>()
                .HasIdentityOptions<int>(1, 1);
        
        builder.Property(x => x.Nome)
                .HasColumnName("nome")
                .HasColumnType("VARCHAR(100)")
                .IsRequired()
                .HasMaxLength(100);
        
        builder.Property(x => x.Descricao)
                .HasColumnName("descricao")
                .HasColumnType("VARCHAR(100)")
                .IsRequired()
                .HasMaxLength(100);

        builder.Property(x => x.DataInicio)
                .HasColumnName("data_inicio")
                .HasColumnType("TIMESTAMP(6)")
                .IsRequired();

        builder.Property(x => x.DataFim)
                .HasColumnName("data_fim")
                .HasColumnType("TIMESTAMP(6)")
                .IsRequired();
        
        builder.Property(x => x.DataCadastro)
                .HasColumnName("data_cadastro")
                .HasColumnType("TIMESTAMP(6)")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.TipoDisciplina)
                .HasColumnName("tipo_disciplina")
                .HasConversion<string>()
                .IsRequired();

        builder.HasMany(x => x.Conteudos)
                .WithOne(x => x.Disciplina)
                .HasForeignKey(x => x.DisciplinaId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        builder.Navigation(x => x.Conteudos)
                .AutoInclude(true);
    }
}