using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.Messages;
using TorneSe.ServicoNotaAlunos.Domain.Notification;
using TorneSe.ServicoNotaAlunos.MessageBus.Messages;
using TorneSe.ServicoNotaAlunos.MessageBus.SQS.Context.Interfaces;

namespace TorneSe.ServicoNotaAlunos.MessageBus.SQS.Clients;
public class LancarNotaAlunoFakeClient : SqsClient<RegistrarNotaAluno>, ILancarNotaAlunoFakeClient
{
    private readonly Queue<QueueMessage<RegistrarNotaAluno>> _filaNotasParaRegistrar;
    private readonly ContextoNotificacao _contextoNotificacao;

    private const string NOME_FILA_CONFIGURACAO = "FilaConfiguracaoFake";

    public LancarNotaAlunoFakeClient(ISqsContext context,ContextoNotificacao contextoNotificacao) : 
            base(context,contextoNotificacao,NOME_FILA_CONFIGURACAO)
    {
        _filaNotasParaRegistrar = NotasParaProcessar();
        _contextoNotificacao = contextoNotificacao;
    }

    public override async Task<QueueMessage<RegistrarNotaAluno>> GetMessageAsync()
    {
        QueueMessage<RegistrarNotaAluno> mensagem = null;
        
        try
        {
            mensagem = await Task.FromResult(_filaNotasParaRegistrar.FirstOrDefault());
        }
        catch(Exception ex)
        {
            _contextoNotificacao.Add(ex.Message);
        }

        return mensagem;
    }
    private Queue<QueueMessage<RegistrarNotaAluno>> NotasParaProcessar()
    {

        var queue = new Queue<QueueMessage<RegistrarNotaAluno>>();
        queue.Enqueue(new()
        {
            MessageId = Guid.NewGuid().ToString(),
            MessageHandle = Guid.NewGuid().ToString(),
            ReceiveCount = 0,
            MessageBody = new RegistrarNotaAluno()
            {
                AlunoId = 1235,
                AtividadeId = 2,
                CorrelationId = Guid.NewGuid(),
                ProfessorId = 1282727,
                ValorNota = 8,
                NotaSubstitutiva = true
            }
        });
        return queue;
    }
}