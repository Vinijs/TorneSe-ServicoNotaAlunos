using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers.Base;
using TorneSe.ServicoNotaAlunos.Domain.Notification;
using TorneSe.ServicoNotaAlunos.Domain.Interfaces.Repositories;
using TorneSe.ServicoNotaAlunos.Domain.Utils;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;

namespace TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers;
public class AlunoRequestBuildHandler : AbstractAsyncHandler<ServicoNotaValidacaoRequest>
{
    private readonly ContextoNotificacao _contextoNotificacao;

    private readonly IUsuarioRepository _usuarioRepository;
    public AlunoRequestBuildHandler(ContextoNotificacao contextoNotificacao,
                                    IUsuarioRepository usuarioRepository)
    {
        _contextoNotificacao = contextoNotificacao;
        _usuarioRepository = usuarioRepository;
    }

    public override async Task Handle(ServicoNotaValidacaoRequest request)
    {
        request.Aluno = await _usuarioRepository.BuscarAluno(request.AlunoId);

            if(request.Aluno is null)
            {
                _contextoNotificacao.Add(Constantes.MensagensExcecao.ALUNO_INEXISTENTE);
                return;
            }
        
        await base.Handle(request);
    }
}
