using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using TorneSe.ServicoNotaAlunos.IOC.Providers;
using TorneSe.ServicoNotaAlunos.Data.Environment;

namespace TorneSe.ServicoNotaAlunos.IOC.Extensions;
public static class HealthChecksExtensions
{
    public static IServiceCollection ConfigurarHealthChecks(this IServiceCollection services,
                                                            IConfiguration configuration,
                                                            IHostEnvironment hostEnvironment)
    {
        var provedorVariaveis = ProvedorVariaveisAmbiente.Instancia;
        var healthCheckBuilder = services
            .AddHealthChecks();

        healthCheckBuilder.AddNpgSql(provedorVariaveis.DefaultConnection, name: "Postgres",
        tags: new string[] { "db", "data" })
        .AddMongoDb(provedorVariaveis.MongoDbUrl, name: "MongoLogs",
        tags: new string[] { "logs", "data" })
        .AddCheck<AwsSqsHealthCheck>("AwsSQS", tags: new string[] { "fila", "mensageria" });

        if (hostEnvironment.IsProduction())
            // healthCheckBuilder.AddElasticsearch(options =>
            // {
            //     options.UseServer(provedorVariaveis.PRD_ElasticSearchUrl);
            //     options.UseBasicAuthentication(provedorVariaveis.ElasticUser, provedorVariaveis.ElasticPassword);
            // }, name: "Elastic"
            // , tags: new string[] { "logs", "logs data" });
             healthCheckBuilder.AddElasticsearch(provedorVariaveis.PRD_ElasticSearchUrl, name: "Elastic"
           , tags: new string[] { "logs", "logs data" });
        else
            healthCheckBuilder.AddElasticsearch(provedorVariaveis.ElasticSearchUrl, name: "Elastic"
           , tags: new string[] { "logs", "logs data" });



        return services;
    }
}