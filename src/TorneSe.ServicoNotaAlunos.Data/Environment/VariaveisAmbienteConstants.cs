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
    public const string DEFAULT_CONNECTION = "PRD_DefaultConnection";
    public const string PRD_ELASTIC_SEARCH_URL = "PRD_ElasticSearchLogs";
    public const string ELASTIC_SEARCH_URL = "ElasticSearchLogs";
    public const string MONGO_DB_URL = "PRD_MongoDbLogs";
    public const string ELASTIC_USER = "ElaticUser";
    public const string ELASTIC_PASSWORD = "ElaticPassword";
    public const string ELASTIC_CLOUD_ID = "ElaticCloudId";

}