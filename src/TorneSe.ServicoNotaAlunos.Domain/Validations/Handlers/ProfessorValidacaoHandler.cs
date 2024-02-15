using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers.Base;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;
using TorneSe.ServicoNotaAlunos.Domain.Notification;
using TorneSe.ServicoNotaAlunos.Domain.Utils;

namespace TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers;
public class ProfessorValidacaoHandler : AbstractHandler<ServicoNotaValidacaoRequest>
{
    private readonly ContextoNotificacao _contextoNotificacao;
    public ProfessorValidacaoHandler(ContextoNotificacao contextoNotificacao)
    {
        _contextoNotificacao = contextoNotificacao;
    }

    public override void Handle(ServicoNotaValidacaoRequest request)
    {
        //o professor deve ser um usuário ativo
        if(!request.Professor.Ativo)
        {
            _contextoNotificacao.Add(Constantes.MensagensValidacao.PROFESSOR_INATIVO);
            return;
        }

        //Deve ministrar a disciplina
        if(!(request.Professor.DisciplinaId == request.Disciplina.Id))
        {
            _contextoNotificacao.Add(Constantes.MensagensValidacao.PROFESSOR_NAO_MINISTRA_A_DISCIPLINA);
            return;
        }

        //Deve ser professor titular e não suplente
        if(!request.Professor.ProfessorTitular && request.Professor.ProfessorSuplente)
        {
            _contextoNotificacao.Add(Constantes.MensagensValidacao.PROFESSOR_DEVE_SER_TITULAR);
            return;
        }

        base.Handle(request);
    }
}