using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TorneSe.ServicoNotaAlunos.IOC.Providers;

namespace TorneSe.ServicoNotaAlunos.IOC.Extensions;
public static class HealthChecksExtensions
{
    public static IServiceCollection ConfigurarHealthChecks(this IServiceCollection services,
                                                            IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("DefaultConnection"), name: "Postgres",
            tags: new string[] {"db", "data"})
            .AddMongoDb(configuration.GetConnectionString("MongoDbLogs"), name: "MongoLogs",
            tags: new string[] {"logs", "data"})
            .AddElasticsearch(configuration.GetConnectionString("ElasticSearchLogs"), name: "ElasticLogs", 
            tags: new string[] {"logs", "observabilidade"})
            .AddCheck<AwsSqsHealthCheck>("AwsSQS", tags: new string[] {"fila", "mensageria"});

        return services;
    }
}