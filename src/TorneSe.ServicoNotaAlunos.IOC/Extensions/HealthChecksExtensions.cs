using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TorneSe.ServicoNotaAlunos.IOC.Providers;
using TorneSe.ServicoNotaAlunos.Data.Environment;

namespace TorneSe.ServicoNotaAlunos.IOC.Extensions;
public static class HealthChecksExtensions
{
    public static IServiceCollection ConfigurarHealthChecks(this IServiceCollection services,
                                                            IConfiguration configuration)
    {
        var provedorVariaveis = ProvedorVariaveisAmbiente.Instancia;
        services
            .AddHealthChecks()
            .AddNpgSql(provedorVariaveis.DefaultConnection, name: "Postgres",
            tags: new string[] {"db", "data"})
            .AddMongoDb(provedorVariaveis.MongoDbUrl, name: "MongoLogs",
            tags: new string[] {"logs", "data"})
            .AddElasticsearch(provedorVariaveis.ElasticSearchUrl, name: "ElasticLogs", 
            tags: new string[] {"logs", "observabilidade"})
            .AddCheck<AwsSqsHealthCheck>("AwsSQS", tags: new string[] {"fila", "mensageria"});

        return services;
    }
}