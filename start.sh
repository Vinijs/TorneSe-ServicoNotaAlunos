$(cat worker.env)
docker run -d -e AccessKey -e SecretAccessKey -e SegundosTempoEspera -e PRD_DefaultConnection \
-e PRD_ElasticSearchLogs -e PRD_MongoDbLogs -e ElasticUser -e ElasticPassword -e ElasticCloudId --name servico-notas tornese/servico-notas