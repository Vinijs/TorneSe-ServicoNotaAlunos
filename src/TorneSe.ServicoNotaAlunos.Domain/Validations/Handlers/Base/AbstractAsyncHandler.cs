using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers.Interfaces;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;

namespace TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers.Base;
public class AbstractAsyncHandler<T> : IAsyncHandler<T> where T : ServicoNotaValidacaoRequest
{
    private IAsyncHandler<T> _nextHandler;
    public virtual async Task Handle(T request)
    {
        if (this._nextHandler != null)
        {
            await this._nextHandler.Handle(request);
        }
    }

    public IAsyncHandler<T> SetNext(IAsyncHandler<T> handler)
    {
        _nextHandler = handler;
        return handler;
    }
}