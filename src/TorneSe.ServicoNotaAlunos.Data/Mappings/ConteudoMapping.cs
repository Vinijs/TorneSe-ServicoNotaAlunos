using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TorneSe.ServicoNotaAlunos.Domain.Entidades;

namespace TorneSe.ServicoNotaAlunos.Data.Mappings;
public class ConteudoMapping : IEntityTypeConfiguration<Conteudo>
{
    public void Configure(EntityTypeBuilder<Conteudo> builder)
    {
        builder.ToTable("conteudos", "servnota")
                .HasKey(x => x.Id);

        builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityAlwaysColumn()
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

        builder.Property(x => x.DataTermino)
                .HasColumnName("data_termino")
                .HasColumnType("TIMESTAMP(6)")
                .IsRequired();

        builder.Property(x => x.DataCadastro)
                .HasColumnName("data_cadastro")
                .HasColumnType("TIMESTAMP(6)")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.DisciplinaId)
                .HasColumnName("disciplina_id")
                .HasColumnType("INT")
                .IsRequired();

        builder.HasOne(x => x.Disciplina)
                .WithMany(x => x.Conteudos)
                .HasForeignKey(x => x.DisciplinaId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasMany(x => x.Atividades)
                .WithOne(x => x.Conteudo)
                .HasForeignKey(x => x.ConteudoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        builder.Navigation(x => x.Atividades)
                .AutoInclude();
    }
}