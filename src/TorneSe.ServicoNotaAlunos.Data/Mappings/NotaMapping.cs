using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TorneSe.ServicoNotaAlunos.Domain.Entidades;

namespace TorneSe.ServicoNotaAlunos.Data.Mappings;
public class NotaMapping : IEntityTypeConfiguration<Nota>
{
    public void Configure(EntityTypeBuilder<Nota> builder)
    {
        builder.ToTable("notas", "servnota")
            .HasKey(x => x.Id);

        builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityAlwaysColumn<int>()
                .HasIdentityOptions<int>(1, 1);

        builder.Property(x => x.AtividadeId)
            .HasColumnName("atividade_id")
            .HasColumnType("int")
            .IsRequired();
        
        builder.Property(x => x.AlunoId)
            .HasColumnName("aluno_id")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(x => x.ValorNota)
               .HasColumnName("valor_nota")
               .HasPrecision(3,2);

        builder.Property(x => x.DataLancamento)
               .HasColumnName("data_lancamento")
               .HasColumnType("timestamp(6)")
               .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.CanceladaPorRetentativa)
               .HasColumnName("cancelada_por_retentativa")
               .HasColumnType("boolean");

        builder.Property(x => x.UsuarioId)
               .HasColumnName("usuario_id")
               .HasColumnType("int")
               .IsRequired();        

        builder.HasOne(x => x.Usuario)
               .WithMany()
               .HasForeignKey(x => x.UsuarioId)
               .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.Atividade)
            .WithMany(x => x.Notas)
            .HasForeignKey(x => x.AtividadeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

         builder.HasOne(x => x.Aluno)
            .WithMany(x => x.Notas)
            .HasForeignKey(x => x.AlunoId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}