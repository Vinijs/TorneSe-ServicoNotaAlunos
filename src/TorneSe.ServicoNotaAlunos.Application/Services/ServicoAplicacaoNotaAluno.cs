using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Application.Interfaces;
using TorneSe.ServicoNotaAlunos.Domain.Interfaces.Services;
using TorneSe.ServicoNotaAlunos.Domain.Messages;
using TorneSe.ServicoNotaAlunos.Domain.Excecoes;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;

namespace TorneSe.ServicoNotaAlunos.Application.Services;
public class ServicoAplicacaoNotaAluno : IServicoAplicacaoNotaAluno
{
    private readonly IServicoNotaAluno _servicoNotaAluno;
    private readonly IUnitOfWork _uow;
    public ServicoAplicacaoNotaAluno(IServicoNotaAluno servicoNotaAluno, 
                                     IUnitOfWork uow)
    {
        _servicoNotaAluno = servicoNotaAluno;
        _uow = uow;
    }
    public async Task ProcessarLancamentoNota(RegistrarNotaAluno registrarNotaAluno)
    {
        try
        {
            Console.WriteLine("Orquestrando o fluxo da aplicação");
            await _servicoNotaAluno.LancarNota(registrarNotaAluno);
            await _uow.Commit();
        }
        catch(DomainException ex)
        {
            System.Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
        
    }        
}

    