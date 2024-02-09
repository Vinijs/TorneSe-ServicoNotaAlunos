using TorneSe.ServicoNotaAlunos.Worker;
using TorneSe.ServicoNotaAlunos.IOC;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        config.AddEnvironmentVariables();
    })
    .ConfigureServices(services =>
    {
        services.ConfigurarInjecaoDependencia()
        .AddHostedService<ServicoNotaAlunoWorker>();
        // .AddHostedService<WorkerExemplo>();
    })
    .Build();

host.Run();
