using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using TorneSe.ServicoNotaAlunos.MessageBus.Messages;
using TorneSe.ServicoNotaAlunos.MessageBus.SQS.Context.Interfaces;
using TorneSe.ServicoNotaAlunos.Domain.Notification;
using Amazon.SQS.Model;

namespace TorneSe.ServicoNotaAlunos.MessageBus.SQS.Clients;
public abstract class SqsClient<T> : IQueueClient<T>
{
    private readonly ISqsContext _context;
    private readonly ContextoNotificacao _contextoNotificacao;
    private readonly string _nomeFila;
    public SqsClient(ISqsContext context,
                     ContextoNotificacao contextoNotificacao,
                     string nomeFila)
    {
        _context = context;
        _contextoNotificacao = contextoNotificacao;
        _nomeFila = context.BuscarUrlFila(nomeFila);
    }

    public virtual async Task DeleteMessageAsync(string messageHandle)
    {
        try
        {
            var deletarMensagem = new DeleteMessageRequest
            {
                QueueUrl = _nomeFila,
                ReceiptHandle = messageHandle
            };

            await _context.Sqs.DeleteMessageAsync(deletarMensagem);
        }
        catch (Exception ex)
        {
            _contextoNotificacao.Add(ex.Message);
            _contextoNotificacao.Add(ex.InnerException?.Message);
            throw;
        }
    }

    public virtual async Task<QueueMessage<T>> GetMessageAsync()
    {
        var mensagem = await GetMessageAsync(1);

        if (mensagem != null && mensagem.Any())
        {
            return mensagem.First();
        }

        return default;
    }

    public virtual async Task<List<QueueMessage<T>>> GetMessageAsync(int count)
    {
        var list = new List<QueueMessage<T>>();

        try
        {
            var receiveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = _nomeFila,
                MaxNumberOfMessages = count > 10 ? 10 : count == 0 ? 1 : count,
                WaitTimeSeconds = _context.SegundosTempoEspera,
                AttributeNames = new List<string>() { "ApproximateReceiveCount" }
            };

            var receiveMessageResponse = await _context.Sqs.ReceiveMessageAsync(receiveMessageRequest);

            if (receiveMessageResponse.Messages.Any())
            {

                foreach (var message in receiveMessageResponse.Messages)
                {
                    var item = new QueueMessage<T>
                    {
                        MessageHandle = message.ReceiptHandle,
                        MessageId = message.MessageId
                    };
                    if (message.Attributes.Any(i => i.Key == "ApproximateReceiveCount"))
                        item.ReceiveCount = Convert.ToInt32(message.Attributes["ApproximateReceiveCount"]);
                    else
                        item.ReceiveCount = 0;

                    if (typeof(T) == typeof(string))
                    {
                        item.MessageBody = (T)Convert.ChangeType(message.Body, typeof(T));
                    }
                    else
                    {
                        item.MessageBody = JsonSerializer.Deserialize<T>(message.Body);
                    }

                    list.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            _contextoNotificacao.Add(ex.Message);
        }

        return list;
    }

    public virtual async Task SendMessageAsync(T message)
    {
        try
        {
            var mensagem = new SendMessageRequest
            {
                QueueUrl = _nomeFila,
                MessageBody = JsonSerializer.Serialize(message)
            };

            await _context.Sqs.SendMessageAsync(mensagem);
        }
        catch (Exception ex)
        {
            _contextoNotificacao.Add(ex.Message);
            _contextoNotificacao.Add(ex.InnerException?.Message);
        }
    }

    public virtual async Task SendMessageAsync(List<T> messageList)
    {
        try
        {
            var messageBatchList = new List<SendMessageBatchRequestEntry>();

            foreach (var message in messageList)
            {
                messageBatchList.Add(new SendMessageBatchRequestEntry()
                {
                    Id = message.GetHashCode().ToString(),
                    MessageBody = JsonSerializer.Serialize(message)
                });
            }

            var sendMessageBatchRequest = new SendMessageBatchRequest
            {
                QueueUrl = _nomeFila,
                Entries = messageBatchList
            };

            await _context.Sqs.SendMessageBatchAsync(sendMessageBatchRequest);
        }
        catch (Exception ex)
        {
            _contextoNotificacao.Add(ex.Message);
            _contextoNotificacao.Add(ex.InnerException?.Message);
            throw;
        }
    }
}