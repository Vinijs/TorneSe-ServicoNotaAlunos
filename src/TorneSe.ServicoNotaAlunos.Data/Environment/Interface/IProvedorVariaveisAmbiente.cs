using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneSe.ServicoNotaAlunos.Data.Environment.Interface;
public interface IProvedorVariaveisAmbiente
{
    string AwsSecret { get; }
    string AwsSecretAccessKey { get; }
    int AwsLongPooling { get; }
    string MongoDbUrl { get; }
    string ElasticSearchUrl { get; }
    string PRD_ElasticSearchUrl { get; }
    string DefaultConnection { get; }
    string ElasticCloudId { get; }
    string ElasticUser { get; }
    string ElasticPassword { get; }
    string Get(string nome);
}