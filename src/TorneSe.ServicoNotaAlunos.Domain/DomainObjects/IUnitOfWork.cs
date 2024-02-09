using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneSe.ServicoNotaAlunos.Domain.DomainObjects;
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }