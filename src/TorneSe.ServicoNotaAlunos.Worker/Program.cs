using TorneSe.ServicoNotaAlunos.Worker;
using TorneSe.ServicoNotaAlunos.IOC;
using TorneSe.ServicoNotaAlunos.Data.Context;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        config.SetBasePath(hostContext.HostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json")
                .AddEnvironmentVariables();

        hostContext.Configuration = config.Build();
    })
    .ConfigureServices((hostContext,services) =>
    {
        services.ConfigurarInjecaoDependencia()
        .AddHostedService<ServicoNotaAlunoWorker>()
        .AddNpgsql<ServicoNotaAlunosContexto>(hostContext.Configuration.GetConnectionString("DefaultConnection"), 
        options => 
        {
            options.CommandTimeout(20);
            options.EnableRetryOnFailure(4,TimeSpan.FromSeconds(20),null);
            // options.UseQuerySplittingBehavior(Microsoft.EntityFrameworkCore.QuerySplittingBehavior.SplitQuery);
        });
        // .AddHostedService<WorkerExemplo>();
    })
    .Build();

host.Run();
