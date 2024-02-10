using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.Notification;
using TorneSe.ServicoNotaAlunos.Domain.Messages;
using TorneSe.ServicoNotaAlunos.Domain.Interfaces.Services;

namespace TorneSe.ServicoNotaAlunos.Domain.Services;
    public class ServicoNotaAluno : IServicoNotaAluno
    {
        private readonly ContextoNotificacao _contextoNotificacao;
        public ServicoNotaAluno(ContextoNotificacao contextoNotificacao)
        {
            _contextoNotificacao = contextoNotificacao;
        }

        public async Task LancarNota(RegistrarNotaAluno registrarNotaAluno)
        {
            Console.WriteLine("Processando lógica de negócio....");

            if(!registrarNotaAluno.MensagemEstaValida())
            {
                _contextoNotificacao.AddRange(registrarNotaAluno.Validacoes.Errors.Select(x => x.ErrorMessage));
                return;
            }
        }        
    }