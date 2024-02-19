using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneSe.ServicoNotaAlunos.Domain.DomainObjects;
    public interface IRepository<T> : IDisposable where T: IRaizAgregacao
    {
    }