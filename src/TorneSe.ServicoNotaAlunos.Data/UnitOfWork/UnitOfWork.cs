using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Data.Context;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;

namespace TorneSe.ServicoNotaAlunos.Data.UnitOfWork;
public class UnitOfWork : IUnitOfWork
{
    public ServicoNotaAlunosContexto _contexto;
    public UnitOfWork(ServicoNotaAlunosContexto contexto)
    {
        _contexto = contexto;

    }

    public async Task<bool> Commit()
    {
        if(_contexto.ContextoPossuiMudancas())
           return await _contexto.SaveChangesAsync() > 0;

        return false;

    }
}