using TorneSe.ServicoNotaAlunos.Worker;
using TorneSe.ServicoNotaAlunos.IOC;
using TorneSe.ServicoNotaAlunos.Data.Context;
using Serilog;

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
        services.ConfigurarInjecaoDependencia(hostContext.Configuration, hostContext.HostingEnvironment)
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
    .UseWindowsService(options =>
    {
        options.ServiceName = ".NET Worker Nota Alunos";
    })
    .UseSerilog()
    .Build();

host.Run();
