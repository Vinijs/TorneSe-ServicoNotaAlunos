$(cat worker.env)
docker run -d -e AccessKey -e SecretAccessKey -e SegundosTempoEspera -e DefaultConnection \
-e ElasticSearchLogs -e MongoDbLogs --name servico-notas tornese/servico-notas