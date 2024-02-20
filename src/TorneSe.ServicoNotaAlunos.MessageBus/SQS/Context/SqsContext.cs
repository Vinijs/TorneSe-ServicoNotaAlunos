using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.MessageBus.SQS.Context.Interfaces;
using Microsoft.Extensions.Configuration;
using Amazon;
using Amazon.SQS;

namespace TorneSe.ServicoNotaAlunos.MessageBus.SQS.Context;
public class SqsContext : ISqsContext
{
    private readonly AmazonSQSClient _sqs;
    private readonly int _segundosTempoEspera;
    private readonly string _awsAccessKey;
    private readonly string _awsSecretAccessKey;

    private readonly IConfiguration _configuration;
    public SqsContext(IConfiguration configuration)
    {
        _configuration = configuration;
        var configuracoesAws = configuration.GetSection("ConfiguracoesAws");
        _awsAccessKey = configuracoesAws.GetValue<string>("AccessKey");
        _awsSecretAccessKey = configuracoesAws.GetValue<string>("SecretAccessKey");
        _segundosTempoEspera = configuracoesAws.GetValue<int>("SegundosTempoEspera");
        _sqs = BuscarClienteAmazonSqs();
    }

    private AmazonSQSClient BuscarClienteAmazonSqs()
    {
        return new AmazonSQSClient(_awsAccessKey,_awsSecretAccessKey,RegionEndpoint.USEast1);
    }

    public AmazonSQSClient Sqs => _sqs;
    public int SegundosTempoEspera => _segundosTempoEspera;

    private string BuscarNomeFila(string nomeFila) =>
        _configuration.GetSection("AwsFilas").GetValue<string>(nomeFila);

    public string BuscarUrlFila(string nomeFila)
    {
        var nomeFilaConfiguracaoAmbiente = BuscarNomeFila(nomeFila);
        return  _sqs.GetQueueUrlAsync(nomeFilaConfiguracaoAmbiente).Result.QueueUrl;
    }

}