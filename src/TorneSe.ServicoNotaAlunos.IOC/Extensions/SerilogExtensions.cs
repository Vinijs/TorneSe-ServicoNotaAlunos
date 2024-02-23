using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NpgsqlTypes;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.PostgreSQL;
using Serilog.Sinks.PostgreSQL.ColumnWriters;

namespace TorneSe.ServicoNotaAlunos.IOC.Extensions;
public static class SerilogExtensions
{
    public static IServiceCollection ConfigurarSerilog(this IServiceCollection services,
                                                         IConfiguration configuration,
                                                         IHostEnvironment hostEnvironment)
    {
        Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Is(Serilog.Events.LogEventLevel.Information)
        .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
        .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Information)
        .Enrich.WithProperty("Application", configuration["Application:ApplicationName"])
        .Enrich.WithProperty("Environment", hostEnvironment.EnvironmentName)
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .Enrich.WithMachineName()
        .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
        // WriteTo.File($"logs/log-{hostEnvironment.EnvironmentName}.txt", rollingInterval: RollingInterval.Day)
        .WriteTo
                    .PostgreSQL(configuration.GetConnectionString("DefaultConnection"), 
                    configuration["PostgresLogs:TableName"], GetColumnsOptions(), restrictedToMinimumLevel:
                    Serilog.Events.LogEventLevel.Information, needAutoCreateTable : true, schemaName : "servnota")
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
}