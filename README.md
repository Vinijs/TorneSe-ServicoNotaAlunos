# Comando para listar os projetos
 - dotnet new --list

 ## Instalando pacotes Nuget apontando a origem
 ``` bash
 - dotnet add package Microsoft.Extensions.DependencyInjection --source https://api.nuget.org/v3/index.json 
 ```
 

# Comando para adcionar projetos a solução vazia
``` bash
A partir de uma solução criada digitamos o comando para adicionar a referência ao csproj dos projetos
    dotnet sln add src\TorneSe.ServicoNotaAlunos.Worker\TorneSe.ServicoNotaAlunos.Worker.csproj
    dotnet sln remove src\TorneSe.ServicoNotaAlunos.Worker\TorneSe.ServicoNotaAlunos.Worker.csproj
```

## Sites para estudo
- https://refactoring.guru/pt-br - Refactoring Guru


## Documentação docker Postgres
- https://hub.docker.com/_/postgres

## Subir o container com Postgres
- docker run -p 5432:5432 -v /c/Users/Vinicius/Documents/database:/var/lib/postgresql/data -e POSTGRES_PASSWORD=1234 -e POSTGRES_USER=torneSe -e POSTGRES_DB=TorneSeDb -d postgres

## Site para encontrar formatos de connections strings
- https://www.connectionstrings.com/

## Postgres DataTypes
- https://www.postgresql.org/docs/8.3/datatype.html

## GUId Generator
- https://www.guidgenerator.com/online-guid-generator.aspx

## Modelo Mensagem Entrada SQS
```
{
    "AlunoId": 1234,
    "ProfessorId": 1282727,
    "AtividadeId": 2,
    "CorrelationId": "fbaa64f1-8e4d-4ff4-b5ff-749dc652c4e7",
    "ValorNota": 8.0,
    "NotaSubstitutiva": true
}
```

## Documentação do Serilog
- https://serilog.net/

## Documentação Serilog Postgres
- https://github.com/serilog-contrib/Serilog.Sinks.Postgresql.Alternative/blob/master/HowToUse.md

## Comparativo Atributos, Asserts de frameworks de testes
- https://xunit.net/docs/comparisons

## Instalação local Windows Service
- https://docs.microsoft.com/pt-br/dotnet/core/extensions/windows-service

## Passo a passo instalação local
- dotnet publish --no-self-contained -o C:\Users\Vinicius\source\repos\publicacao -p:PublishProfile=FolderProfile
- sc.exe create "Servico Integracao Notas" binpath="C:\Users\Vinicius\source\repos\publicacao\TorneSe.ServicoNotaAlunos.Worker.exe"
- sc.exe failure "Servico Integracao Notas" reset=0 actions=restart/60000/restart/60000/run/1000
- sc.exe start "Servico Integracao Notas"
- sc.exe stop "Servico Integracao Notas"
- sc.exe delete "Servico Integracao Notas"

## Criar imagem docker aplicação
- docker build -t tornese/servico-notas:latest .
- docker run -d --name servico-notas tornese/servico-notas

## Criar conta no Atlas
- https://account.mongodb.com/account/login?n=%2Fv2%2F5e8c9673dce91c238d9046bc%23clusters

## Exemplo de conexão cluster mongo
mongodb://:@ac-2fgps34-shard-00-00.1tdhbqq.mongodb.net:27017,ac-2fgps34-shard-00-01.1tdhbqq.mongodb.net:27017,ac-2fgps34-shard-00-02.1tdhbqq.mongodb.net:27017/<database_name>?ssl=true&replicaSet=atlas-6bk87u-shard-0&authSource=admin&retryWrites=true&w=majority

https://worker-elastic.es.us-east-1.aws.found.io:9243

## Comandos após provisionar a maquina linux
- sudo apt-get update
- sudo apt-get upgrade -y
- sudo apt-get install docker-compose -y
- vim worker.env
- docker build -t tornese/servico-notas:latest .

## Executando background service .net linux
- https://developpaper.com/build-cross-platform-net-core-background-service/
- https://rafaelcruz.azurewebsites.net/2020/07/07/construindo-um-windows-service-ou-linux-daemon-com-worker-service-net-core-parte-2/

## COmandos para baixar pacotes e instalar sdk .net
- sudo wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
- sudo dpkg -i packages-microsoft-prod.deb
- sudo rm packages-microsoft-prod.deb
- SDK : sudo apt-get update;
sudo apt-get install -y apt-transport-https &&
sudo apt-get update &&
sudo apt-get install -y dotnet-sdk-6.0
- Runtime : sudo apt-get update;
sudo apt-get install -y apt-transport-https &&
sudo apt-get update &&
sudo apt-get install -y aspnetcore-runtime-6.0