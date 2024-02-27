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