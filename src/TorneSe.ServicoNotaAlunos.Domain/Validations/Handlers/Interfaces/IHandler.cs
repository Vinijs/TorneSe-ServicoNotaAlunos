using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers.Interfaces;
public interface IHandler<T>
{
    IHandler<T> SetNext(IHandler<T> handler);

    void Handle(T request);
}