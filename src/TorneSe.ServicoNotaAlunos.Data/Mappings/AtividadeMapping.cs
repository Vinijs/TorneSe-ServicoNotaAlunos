using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TorneSe.ServicoNotaAlunos.Domain.Entidades;

namespace TorneSe.ServicoNotaAlunos.Data.Mappings;
public class AtividadeMapping : IEntityTypeConfiguration<Atividade>
{
    public void Configure(EntityTypeBuilder<Atividade> builder)
    {
        builder.ToTable("atividades", "servnota");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .UseIdentityAlwaysColumn()
            .HasIdentityOptions<int>(1,1);

        builder.Property(x => x.Descricao)
            .HasColumnType("VARCHAR(255)")
            .HasColumnName("descricao")
            .IsRequired();

        builder.Property(x => x.TipoAtividade)
            .HasColumnName("tipo_atividade")    
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.DataAtividade)
            .HasColumnName("data_atividade")
            .HasColumnType("TIMESTAMP(6)")
            .IsRequired();

        builder.Property(x => x.DataCadastro)
            .HasColumnType("TIMESTAMP(6)")
            .HasColumnName("data_cadastro")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(x => x.PossuiRetentativa)
            .HasColumnType("BOOLEAN")
            .HasColumnName("possui_retentativa")
            .HasDefaultValueSql("FALSE")
            .IsRequired();

        builder.Property(x => x.ConteudoId)
                .HasColumnName("conteudo_id")
                .HasColumnType("INT")
                .IsRequired();

        builder.HasOne(x => x.Conteudo)
                .WithMany(x => x.Atividades)
                .HasForeignKey(x => x.ConteudoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasMany(x => x.Notas)
                .WithOne(x => x.Atividade)
                .HasForeignKey(x => x.AtividadeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
    }
}