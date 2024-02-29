#Imagem sdk para compilar
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore "src/TorneSe.ServicoNotaAlunos.Worker/TorneSe.ServicoNotaAlunos.Worker.csproj"
# Build and publish a release
RUN dotnet build "src/TorneSe.ServicoNotaAlunos.Worker/TorneSe.ServicoNotaAlunos.Worker.csproj" -c Release -o /app/build -r linux-x64 --no-self-contained

#Imagem runtime para executar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/build .
ENTRYPOINT ["dotnet", "TorneSe.ServicoNotaAlunos.Worker.dll"]