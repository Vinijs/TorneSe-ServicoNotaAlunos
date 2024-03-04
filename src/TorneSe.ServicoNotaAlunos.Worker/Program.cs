using System.Runtime.InteropServices;
using TorneSe.ServicoNotaAlunos.Worker;
using TorneSe.ServicoNotaAlunos.IOC;
using TorneSe.ServicoNotaAlunos.Data.Context;
using TorneSe.ServicoNotaAlunos.Data.Environment;
using Serilog;

IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args)
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
        .AddNpgsql<ServicoNotaAlunosContexto>(ProvedorVariaveisAmbiente.Instancia.DefaultConnection, 
        options => 
        {
            options.CommandTimeout(20);
            options.EnableRetryOnFailure(4,TimeSpan.FromSeconds(20),null);
            // options.UseQuerySplittingBehavior(Microsoft.EntityFrameworkCore.QuerySplittingBehavior.SplitQuery);
        });
        // .AddHostedService<WorkerExemplo>();
    })
    .UseSerilog();

    if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
        hostBuilder.UseWindowsService(options =>
        {
            options.ServiceName = "Servico Integracao Notas";
        });
    }
    else
    {
        hostBuilder.UseSystemd();
    }
    
await hostBuilder.Build().RunAsync();
