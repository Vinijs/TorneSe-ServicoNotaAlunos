using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.Messages;
using TorneSe.ServicoNotaAlunos.MessageBus.Messages;

namespace TorneSe.ServicoNotaAlunos.Application.Interfaces;
public interface INotaAlunoReceberMensagemService
{
    Task<QueueMessage<RegistrarNotaAluno>> BuscarMensagem();
    Task DeletarMensagem(string messageHandle);
}