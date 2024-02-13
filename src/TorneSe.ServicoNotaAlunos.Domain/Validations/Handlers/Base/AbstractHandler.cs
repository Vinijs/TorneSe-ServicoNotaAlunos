using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers.Interfaces;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;

namespace TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers.Base;
public abstract class AbstractHandler<T> : IHandler<T> where T : ServicoNotaValidacaoRequest
{
    private IHandler<T> _nextHandler;
    public virtual void Handle(T request)
    {
        if (this._nextHandler != null)
        {
            this._nextHandler.Handle(request);
        }
    }

    public IHandler<T> SetNext(IHandler<T> handler)
    {
        _nextHandler = handler;
        return handler;
    }
}