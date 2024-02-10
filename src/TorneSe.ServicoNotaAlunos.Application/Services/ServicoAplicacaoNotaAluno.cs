using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Application.Interfaces;
using TorneSe.ServicoNotaAlunos.Domain.Interfaces.Services;
using TorneSe.ServicoNotaAlunos.Domain.Messages;

namespace TorneSe.ServicoNotaAlunos.Application.Services;
public class ServicoAplicacaoNotaAluno : IServicoAplicacaoNotaAluno
{
    private readonly IServicoNotaAluno _servicoNotaAluno;
    public ServicoAplicacaoNotaAluno(IServicoNotaAluno servicoNotaAluno)
    {
        _servicoNotaAluno = servicoNotaAluno;
    }
    public async Task ProcessarLancamentoNota(RegistrarNotaAluno registrarNotaAluno)
    {
        Console.WriteLine("Orquestrando o fluxo da aplicação");
        await _servicoNotaAluno.LancarNota(registrarNotaAluno);
        
    }        
}

    