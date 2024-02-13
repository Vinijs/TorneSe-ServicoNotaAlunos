using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers.Interfaces;
public interface IAsyncHandler<T>
{
    IAsyncHandler<T> SetNext(IAsyncHandler<T> handler);

    Task Handle(T request);
}