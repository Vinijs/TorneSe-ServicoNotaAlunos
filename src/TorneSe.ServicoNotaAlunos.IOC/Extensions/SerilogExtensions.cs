using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Elasticsearch.Net;
using NpgsqlTypes;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.PostgreSQL;
using Serilog.Sinks.PostgreSQL.ColumnWriters;
using Serilog.Sinks.Elasticsearch;
using Serilog.Events;
using TorneSe.ServicoNotaAlunos.Data.Environment;

namespace TorneSe.ServicoNotaAlunos.IOC.Extensions;
public static class SerilogExtensions
{
    public static IServiceCollection ConfigurarSerilog(this IServiceCollection services,
                                                         IConfiguration configuration,
                                                         IHostEnvironment hostEnvironment)
    {
        var provedorVariaveis = ProvedorVariaveisAmbiente.Instancia;
        var indexFormat = $"{configuration["Application:ApplicationName"]}-logs-{hostEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.Now:yyyy-MM-dd}";
        Log.Logger = new LoggerConfiguration()
        .Enrich.WithProperty("Application", configuration["Application:ApplicationName"])
        .Enrich.WithProperty("Environment", hostEnvironment.EnvironmentName)
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .Enrich.WithMachineName()
        .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
        // WriteTo.File($"logs/log-{hostEnvironment.EnvironmentName}.txt", rollingInterval: RollingInterval.Day)
        // .WriteTo
        //             .PostgreSQL(provedorVariaveis.DefaultConnection), 
        //             configuration["PostgresLogs:TableName"], GetColumnsOptions(), restrictedToMinimumLevel:
        //             Serilog.Events.LogEventLevel.Information, needAutoCreateTable : true, schemaName : "servnota")
        .WriteTo.MongoDB(provedorVariaveis.MongoDbUrl, collectionName: "logs-notas")
        .WriteTo.Elasticsearch(GetElasticsearchSinkOptions(configuration, hostEnvironment))
        .ReadFrom.Configuration(configuration)
        .CreateLogger();

        return services;
    }

    private static IDictionary<string, ColumnWriterBase> GetColumnsOptions()
    {
        return new Dictionary<string, ColumnWriterBase>
        {
            { "message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
            { "message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
            { "level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
            { "raise_date", new TimestampColumnWriter(NpgsqlDbType.TimestampTz) },
            { "exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
            { "properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) },
            { "props_test", new PropertiesColumnWriter(NpgsqlDbType.Jsonb) },
            { "machine_name", new SinglePropertyColumnWriter("MachineName", PropertyWriteMethod.ToString, NpgsqlDbType.Text, "l") }
        };
    }

    private static ElasticsearchSinkOptions GetElasticsearchSinkOptions(IConfiguration configuration,
                                                                        IHostEnvironment hostEnvironment)
    {
        if (hostEnvironment.IsProduction())
            return new ElasticsearchSinkOptions(new Uri(ProvedorVariaveisAmbiente.Instancia.PRD_ElasticSearchUrl))
            {
                MinimumLogEventLevel = LogEventLevel.Information,
                IndexFormat = $"{configuration["Application:ApplicationName"]}-logs-{hostEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.Now:yyyy-MM-dd}",
                AutoRegisterTemplate = true,
                NumberOfShards = 2,
                NumberOfReplicas = 1,
            };
            // return new ElasticsearchSinkOptions()
            // {
            //     MinimumLogEventLevel = LogEventLevel.Information,
            //     //Importante o nome do index deve ser separado por - e somente letras minusculas para atender ao pattern
            //     IndexFormat = $"{configuration["Application:ApplicationName"]}-logs-{hostEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.Now:yyyy-MM-dd}",
            //     AutoRegisterTemplate = true,
            //     NumberOfShards = 2,
            //     NumberOfReplicas = 1,
            //     ModifyConnectionSettings = conn =>
            //     {
            //         var basicCredential = new BasicAuthenticationCredentials(ProvedorVariaveisAmbiente.Instancia.ElasticUser, ProvedorVariaveisAmbiente.Instancia.ElasticPassword);
            //         return new ConnectionConfiguration(ProvedorVariaveisAmbiente.Instancia.ElasticCloudId, basicCredential);
            //     }
            // };
        else
            return new ElasticsearchSinkOptions(new Uri(ProvedorVariaveisAmbiente.Instancia.ElasticSearchUrl))
            {
                MinimumLogEventLevel = LogEventLevel.Information,
                IndexFormat = $"{configuration["Application:ApplicationName"]}-logs-{hostEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.Now:yyyy-MM-dd}",
                AutoRegisterTemplate = true,
                NumberOfShards = 2,
                NumberOfReplicas = 1,
            };
    }
}