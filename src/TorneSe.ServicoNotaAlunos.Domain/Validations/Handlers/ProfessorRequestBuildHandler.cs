using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.Notification;
using TorneSe.ServicoNotaAlunos.Domain.Interfaces.Repositories;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;
using TorneSe.ServicoNotaAlunos.Domain.Utils;
using TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers.Base;

namespace TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers;
public class ProfessorRequestBuildHandler : AbstractAsyncHandler<ServicoNotaValidacaoRequest>
{
     private readonly ContextoNotificacao _contextoNotificacao;

    private readonly IUsuarioRepository _usuarioRepository;
    public ProfessorRequestBuildHandler(ContextoNotificacao contextoNotificacao,
                                    IUsuarioRepository usuarioRepository)
    {
        _contextoNotificacao = contextoNotificacao;
        _usuarioRepository = usuarioRepository;
    }

    public override async Task Handle(ServicoNotaValidacaoRequest request)
    {
        request.Professor = await _usuarioRepository.BuscarProfessorDb(request.ProfessorId);

            if(request.Professor is null)
            {
                _contextoNotificacao.Add(Constantes.MensagensExcecao.PROFESSOR_INEXISTENTE);
                return;
            }
        
        await base.Handle(request);
    }
}