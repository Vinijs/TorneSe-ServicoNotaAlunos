using Microsoft.Extensions.Diagnostics.HealthChecks;
using TorneSe.ServicoNotaAlunos.Application.Interfaces;
using TorneSe.ServicoNotaAlunos.Domain.Utils;

namespace TorneSe.ServicoNotaAlunos.Worker;

public class ServicoNotaAlunoWorker : BackgroundService
{
    private readonly ILogger<ServicoNotaAlunoWorker> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly HealthCheckService _healthCheckService;

    public ServicoNotaAlunoWorker(ILogger<ServicoNotaAlunoWorker> logger,
                                  IServiceScopeFactory serviceScopeFactory,
                                  HealthCheckService healthCheckService)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _healthCheckService = healthCheckService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var healths = await _healthCheckService.CheckHealthAsync(stoppingToken);
        if (healths.Status == HealthStatus.Healthy)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation(Constantes.MensagensAplicacao.INICIANDO_SERVICO);
                using var scope = _serviceScopeFactory.CreateScope();
                var servicoNotaAlunoApp = scope.ServiceProvider.GetRequiredService<IServicoAplicacaoNotaAluno>();

                await servicoNotaAlunoApp.ProcessarLancamentoNota();
            }
        }
    }
}
