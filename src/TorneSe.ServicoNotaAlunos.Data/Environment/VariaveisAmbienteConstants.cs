using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneSe.ServicoNotaAlunos.Data.Environment;
public class VariaveisAmbienteConstants
{
    public const string AWS_SECRET = "AccessKey";
    public const string AWS_SECRET_KEY = "SecretAccessKey";
    public const string SEGUNDOS_POLLING_AWS = "SegundosTempoEspera";
    public const string DEFAULT_CONNECTION = "DefaultConnection";
    public const string ELASTIC_SEARCH_URL = "ElasticSearchLogs";
    public const string MONGO_DB_URL = "MongoDbLogs";
}