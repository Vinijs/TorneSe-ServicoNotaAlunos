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
        throw new NotImplementedException();
    }
}