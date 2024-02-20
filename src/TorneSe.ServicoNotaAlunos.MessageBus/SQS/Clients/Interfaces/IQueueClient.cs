using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.MessageBus.Messages;

namespace TorneSe.ServicoNotaAlunos.MessageBus.SQS.Clients;
public interface IQueueClient<T>
{
    Task SendMessageAsync(T message);
    Task<QueueMessage<T>> GetMessageAsync();
    Task<List<QueueMessage<T>>> GetMessageAsync(int count);
    Task DeleteMessageAsync(string MessageHandle);
}