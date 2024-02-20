using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.Messages;

namespace TorneSe.ServicoNotaAlunos.MessageBus.SQS.Clients.Interfaces;
public interface ILancarNotaAlunoRecebimentoClient : IQueueClient<RegistrarNotaAluno>{}
