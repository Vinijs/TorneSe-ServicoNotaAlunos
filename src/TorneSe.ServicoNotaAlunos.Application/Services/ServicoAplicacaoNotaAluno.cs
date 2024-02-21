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

namespace TorneSe.ServicoNotaAlunos.Application.Services;
public class ServicoAplicacaoNotaAluno : IServicoAplicacaoNotaAluno
{
    private readonly IServicoNotaAluno _servicoNotaAluno;
    private readonly IUnitOfWork _uow;
    private readonly INotaAlunoReceberMensagemService _notaAlunoReceberMensagem;
    private readonly ContextoNotificacao _contextoNotificacao;
    public ServicoAplicacaoNotaAluno(IServicoNotaAluno servicoNotaAluno,
                                     IUnitOfWork uow,
                                     INotaAlunoReceberMensagemService notaAlunoReceberMensagem,
                                     ContextoNotificacao contextoNotificacao)
    {
        _servicoNotaAluno = servicoNotaAluno;
        _uow = uow;
        _notaAlunoReceberMensagem = notaAlunoReceberMensagem;
        _contextoNotificacao = contextoNotificacao;
    }
    public async Task ProcessarLancamentoNota()
    {
        try
        {
            var mensagem = await _notaAlunoReceberMensagem.BuscarMensagem();
            if (_contextoNotificacao.TemNotificacoes)
            {
                Console.WriteLine(_contextoNotificacao.ToJson());
                return;
            }

            if (mensagem is null)
            {
                Console.WriteLine(Constantes.MensagensAplicacao.SEM_MENSAGEM_NA_FILA);
                return;
            }
            Console.WriteLine("Orquestrando o fluxo da aplicação");
            await _servicoNotaAluno.LancarNota(mensagem.MessageBody);
            await _uow.Commit();

            if (_contextoNotificacao.TemNotificacoes)
                Console.WriteLine(_contextoNotificacao.ToJson());
            else
                Console.WriteLine($"Mensagem de identificador: {mensagem.MessageId}, processada com sucesso");

            await _notaAlunoReceberMensagem.DeletarMensagem(mensagem.MessageHandle);
        }
        catch (DomainException ex)
        {
            System.Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }

    }
}

