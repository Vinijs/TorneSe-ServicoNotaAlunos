using TorneSe.ServicoNotaAlunos.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddHostedService<WorkerExemplo>();
    })
    .Build();

host.Run();
