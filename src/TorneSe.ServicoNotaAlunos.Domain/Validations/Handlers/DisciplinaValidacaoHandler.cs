using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers.Base;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;
using TorneSe.ServicoNotaAlunos.Domain.Notification;
using TorneSe.ServicoNotaAlunos.Domain.Enums;
using TorneSe.ServicoNotaAlunos.Domain.Utils;
using TorneSe.ServicoNotaAlunos.Domain.Entidades;

namespace TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers;
public class DisciplinaValidacaoHandler : AbstractHandler<ServicoNotaValidacaoRequest>
{
    private readonly ContextoNotificacao _contextoNotificacao;
    public DisciplinaValidacaoHandler(ContextoNotificacao contextoNotificacao)
    {
        _contextoNotificacao = contextoNotificacao;
    }

    public override void Handle(ServicoNotaValidacaoRequest request)
    {
        //A disciplina não pode ser do tipo encontro
        if(request.Disciplina.TipoDisciplina == TipoDisciplina.Encontro)
        {
            _contextoNotificacao.Add(Constantes.MensagensValidacao.DISCIPLINA_TIPO_ENCONTRO);
            return;
        }

        //A disciplina deve estar ativa
        if(!DisciplinaAtiva(request.Disciplina))
        {
            _contextoNotificacao.Add(Constantes.MensagensValidacao.DISCIPLINA_INATIVA);
            return;
        }

        base.Handle(request);
    }

     private bool DisciplinaAtiva(Disciplina disciplina) =>
        disciplina.DataInicio <= DateTime.Now && disciplina.DataFim >= DateTime.Now;
}