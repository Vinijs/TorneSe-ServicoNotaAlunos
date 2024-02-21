using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Application.Interfaces;
using TorneSe.ServicoNotaAlunos.Domain.Messages;
using TorneSe.ServicoNotaAlunos.MessageBus.Messages;
using TorneSe.ServicoNotaAlunos.MessageBus.SQS.Clients.Interfaces;

namespace TorneSe.ServicoNotaAlunos.Application.Services;
public class NotaAlunoReceberMensagemService : INotaAlunoReceberMensagemService
{
    private readonly ILancarNotaAlunoRecebimentoClient _lancarNotaAlunoRecebimento;
    public NotaAlunoReceberMensagemService(ILancarNotaAlunoRecebimentoClient lancarNotaAlunoRecebimento)
    {
        _lancarNotaAlunoRecebimento = lancarNotaAlunoRecebimento;
    }

    public async Task<QueueMessage<RegistrarNotaAluno>> BuscarMensagem() =>
        await _lancarNotaAlunoRecebimento.GetMessageAsync();

    public async Task DeletarMensagem(string messageHandle)
    {
        await _lancarNotaAlunoRecebimento.DeleteMessageAsync(messageHandle);
    }
}