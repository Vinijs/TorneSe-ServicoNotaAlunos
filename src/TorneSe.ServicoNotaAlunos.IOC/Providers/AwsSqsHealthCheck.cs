using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using TorneSe.ServicoNotaAlunos.MessageBus.SQS.Context.Interfaces;

namespace TorneSe.ServicoNotaAlunos.IOC.Providers;
public class AwsSqsHealthCheck : IHealthCheck
{
    private readonly ISqsContext _sqsContext;
    private const string NOME_FILA_RECEBIMENTO = "FilaConfiguracaoRecebimento";
    private const string NOME_FILA_RESPOSTA = "FilaConfiguracaoResposta";
    public AwsSqsHealthCheck(ISqsContext sqsContext)
    {
        _sqsContext = sqsContext;
    }
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken)
    {
        if(string.IsNullOrEmpty(BuscarFilaRecebimento()) || string.IsNullOrEmpty(BuscarFilaResposta()))
        {
            return Task.FromResult(HealthCheckResult.Unhealthy());
        }

        return Task.FromResult(HealthCheckResult.Healthy());
    }

    private string BuscarFilaRecebimento() =>
        _sqsContext.BuscarUrlFila(NOME_FILA_RECEBIMENTO);

    private string BuscarFilaResposta() =>
        _sqsContext.BuscarUrlFila(NOME_FILA_RESPOSTA);
}