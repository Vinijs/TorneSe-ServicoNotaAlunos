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