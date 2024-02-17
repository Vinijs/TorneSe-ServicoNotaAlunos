using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers.Base;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;
using TorneSe.ServicoNotaAlunos.Domain.Notification;
using TorneSe.ServicoNotaAlunos.Domain.Interfaces.Repositories;
using TorneSe.ServicoNotaAlunos.Domain.Utils;

namespace TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers;
public class DisciplinaRequestBuildHandler : AbstractAsyncHandler<ServicoNotaValidacaoRequest>
{
    private readonly ContextoNotificacao _contextoNotificacao;

    private readonly IDisciplinaRepository _disciplinaRepository;
    public DisciplinaRequestBuildHandler(ContextoNotificacao contextoNotificacao,
                                    IDisciplinaRepository disciplinaRepository)
    {
        _contextoNotificacao = contextoNotificacao;
        _disciplinaRepository = disciplinaRepository;
    }

    public override async Task Handle(ServicoNotaValidacaoRequest request)
    {
        request.Disciplina = await _disciplinaRepository.BuscarDisciplinaPorAtividadeIdDb(request.AtividadeId);

        if (request.Disciplina is null)
        {
            _contextoNotificacao.Add(Constantes.MensagensExcecao.DISCIPLINA_INEXISTENTE);
            return;
        }

        await base.Handle(request);
    }
}