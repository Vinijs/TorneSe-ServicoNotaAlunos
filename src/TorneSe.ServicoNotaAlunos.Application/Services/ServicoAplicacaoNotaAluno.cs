using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Application.Interfaces;
using TorneSe.ServicoNotaAlunos.Domain.Interfaces.Services;
using TorneSe.ServicoNotaAlunos.Domain.Excecoes;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;
using TorneSe.ServicoNotaAlunos.Domain.Notification;
using TorneSe.ServicoNotaAlunos.Domain.Utils;
using Microsoft.Extensions.Logging;

namespace TorneSe.ServicoNotaAlunos.Application.Services;
public class ServicoAplicacaoNotaAluno : IServicoAplicacaoNotaAluno
{
    private readonly IServicoNotaAluno _servicoNotaAluno;
    private readonly IUnitOfWork _uow;
    private readonly INotaAlunoReceberMensagemService _notaAlunoReceberMensagem;
    readonly INotaAlunoRespostaMensagemService _notaAlunoRespostaMensagem;
    private readonly ContextoNotificacao _contextoNotificacao;
    private readonly ILogger<ServicoAplicacaoNotaAluno> _logger;
    public ServicoAplicacaoNotaAluno(IServicoNotaAluno servicoNotaAluno,
                                     IUnitOfWork uow,
                                     INotaAlunoReceberMensagemService notaAlunoReceberMensagem,
                                     ContextoNotificacao contextoNotificacao,
                                     INotaAlunoRespostaMensagemService notaAlunoRespostaMensagem,
                                     ILogger<ServicoAplicacaoNotaAluno> logger)
    {
        _servicoNotaAluno = servicoNotaAluno;
        _uow = uow;
        _notaAlunoReceberMensagem = notaAlunoReceberMensagem;
        _contextoNotificacao = contextoNotificacao;
        _notaAlunoRespostaMensagem = notaAlunoRespostaMensagem;
        _logger = logger;
    }
    public async Task ProcessarLancamentoNota()
    {
        try
        {
            var mensagem = await _notaAlunoReceberMensagem.BuscarMensagem();

            _logger.LogInformation("Orquestrando o fluxo da aplicação");

            if (_contextoNotificacao.TemNotificacoes)
            {
                Console.WriteLine(_contextoNotificacao.ToJson());
                return;
            }

            if (mensagem is null)
            {
                _logger.LogInformation(Constantes.MensagensAplicacao.SEM_MENSAGEM_NA_FILA);
                return;
            }

            _logger.LogInformation("Recebendo mensagem para processar: {@Mensagem}", mensagem.MessageBody);
            
            _logger.LogInformation("Iniciando processamento da mensagem: {Identificador} {Data}", mensagem.MessageId, DateTime.Now.ToString());
            await _servicoNotaAluno.LancarNota(mensagem.MessageBody);

            if(!await _uow.Commit())
            {
                _contextoNotificacao.Add(Constantes.MensagensAplicacao.NAO_HOUVE_MUDANCAS_NO_PROCESSAMENTO);
            }

            await _notaAlunoReceberMensagem.DeletarMensagem(mensagem.MessageHandle);
            await _notaAlunoRespostaMensagem.EnviarMensagem(mensagem.MessageBody);

            if (_contextoNotificacao.TemNotificacoes)
                _logger.LogWarning(_contextoNotificacao.ToJson());
            else
                _logger.LogInformation($"Mensagem de identificador: {mensagem.MessageId}, processada com sucesso " + DateTime.Now.ToString());
        }
        catch (DomainException ex)
        {
            _logger.LogError(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.Message);
        }

    }
}

