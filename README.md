# Serviço Integração de Notas Torne Se Um Programador

## Worker Integração de Notas :trophy:

    Serviço instalável ou executável com finalidade de consumo para mensagens tornando o processo de lançamento de notas assíncrono entre base e dados



## Como funciona? :bulb:

    Sempre que adicionada uma mensagem na fila de integração de notas o worker é responsável por processar a mesma, executando validações das regras definidas e transportando o valor das notas lançadas para outro banco que centraliza os dados.




## Desenho fluxo aplicação: 

![FluxoAplicacaoWorker](https://user-images.githubusercontent.com/52010253/176569653-1ab2f8a8-880d-4575-a1f5-ad63434897e6.png)


## Pré requisitos :warning:

- [.Net 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Entity Framework Core](https://docs.microsoft.com/pt-br/ef/core/overview)
- [Docker](https://www.docker.com/)****
- [Postgres](https://www.postgresql.org/docs)
- [Sqs](https://aws.amazon.com/pt/sqs/)

## Configuração de ambiente:

 Para executar a aplicação em ambiente de desenvolvimento basta executar o arquivo docker-compose.yaml dentro da pasta deploy. Obs: Deve-se configurar as variáveis de ambiente no arquivo worker.env inclusive AWS SQS, seguir env worker-env-example

```bash
    docker-compose -f docker-compose.yaml up -d
```

## Comandos para executar a aplicação

```bash
    dotnet run
```

## Diagrama de classes do banco de dados :floppy_disk:

![DiagramaBancoPostgres](https://user-images.githubusercontent.com/52010253/176571305-9d2f4cfd-5d33-4180-9e8f-8038986cb9b4.png)

## Modelo dados mensagem lançamento nota:

```json
{
    "AlunoId": 1234,
    "ProfessorId": 1282727,
    "AtividadeId": 2,
    "CorrelationId": "fbaa64f1-8e4d-4ff4-b5ff-749dc652c4e7",
    "ValorNota": 8.0,
    "NotaSubstitutiva": true
}
```

## Passo a passo instalação: :exclamation:

### Windows 
    - dotnet publish --no-self-contained -o C:\Users\Vinicius\source\repos\publicacao -p:PublishProfile=FolderProfile
    - sc.exe create "Servico Integracao Notas" binpath="C:\Users\Vinicius\source\repos\publicacao\TorneSe.ServicoNotaAlunos.Worker.exe"
    - sc.exe failure "Servico Integracao Notas" reset=0 actions=restart/60000/restart/60000/run/1000
    - sc.exe start "Servico Integracao Notas"
    - sc.exe stop "Servico Integracao Notas"
    - sc.exe delete "Servico Integracao Notas"

### Linux Container
    - sudo apt-get update
    - sudo apt-get upgrade -y
    - sudo apt-get install docker-compose -y
    - vim worker.env
    - docker build -t tornese/servico-notas:latest .

### Linux
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
    - dotnet build "src/TorneSe.ServicoNotaAlunos.Worker/TorneSe.ServicoNotaAlunos.Worker.csproj" -c Release -o /etc/systemd/system -r linux-x64 --no-self-contained
    - path = /root/app/build/TorneSe.ServicoNotaAlunos.Worker.dll

## Dependências: :package:
| Nuget | Documentação |
| --- | --- |
| FluentAssertions | <https://fluentassertions.com/> |
| FluentValidation | <https://github.com/FluentValidation/FluentValidation> |
| Entity Framework Core | <https://docs.microsoft.com/pt-br/ef/core/overview> |
| Serilog | <https://serilog.net/> |
| Xunit | <https://xunit.github.io/> |
| Sqs | <https://aws.amazon.com/pt/sqs/> |


## Dicas de estudo, documentações e links auxiliares :bookmark_tabs:

- [Refactoring Guru](https://refactoring.guru/pt-br)
- [Connection Strings](https://www.connectionstrings.com/)
- [Guid Generator](https://www.guidgenerator.com/online-guid-generator.aspx)
- [Serilog](https://serilog.net/)
- [Serilog Postgres](https://github.com/serilog-contrib/Serilog.Sinks.Postgresql.Alternative/blob/master/HowToUse.md)
- [Asserts Testes de Unidade](https://xunit.net/docs/comparisons)
- [Instalação Windows Service](https://docs.microsoft.com/pt-br/dotnet/core/extensions/windows-service)
- [Mongo Db Atlas](https://account.mongodb.com/account/login?n=%2Fv2%2F5e8c9673dce91c238d9046bc%23clusters)
- [Artigo Serviços Linux 1](https://developpaper.com/build-cross-platform-net-core-background-service/)
- [Artigo Serviços Linux 2](https://rafaelcruz.azurewebsites.net/2020/07/07/construindo-um-windows-service-ou-linux-daemon-com-worker-service-net-core-parte-2/)

## Comandos uteis durante o desenvolvimento:

```bash
    # listar tipos projetos
    dotnet new --list

    # instalar pacotes apontando a origem
    dotnet add package Microsoft.Extensions.DependencyInjection --source https://api.nuget.org/v3/index.json 

    dotnet sln add src\TorneSe.ServicoNotaAlunos.Worker\TorneSe.ServicoNotaAlunos.Worker.csproj
    dotnet sln remove src\TorneSe.ServicoNotaAlunos.Worker\TorneSe.ServicoNotaAlunos.Worker.csproj

    docker run -p 5432:5432 -v /c/Users/Vinicius/Documents/database:/var/lib/postgresql/data -e POSTGRES_PASSWORD=1234 -e POSTGRES_USER=torneSe -e POSTGRES_DB=TorneSeDb -d postgres

    docker build -t tornese/servico-notas:latest .
    docker run -d --name servico-notas tornese/servico-notas
```

### Exemplo conexão mongo:

    mongodb://:@ac-2fgps34-shard-00-00.1tdhbqq.mongodb.net:27017,ac-2fgps34-shard-00-01.1tdhbqq.mongodb.net:27017,ac-2fgps34-shard-00-02.1tdhbqq.mongodb.net:27017/<database_name>?ssl=true&replicaSet=atlas-6bk87u-shard-0&authSource=admin&retryWrites=true&w=majority

## Testes: :test_tube:

    Para executar os testes na aplicação basta executar o comando 
    ```bash
        dotnet test
    ```

## Duvidas: :envelope:

    Para dúvidas, sugestões ou quaisquer dificuldades na implementação da solução basta mandar mensagem chat do Discord do Torne Se Um Programador na aba de CSharp, sinta-se a vontade para alterar, melhorar e enviar pull request para o projeto.  