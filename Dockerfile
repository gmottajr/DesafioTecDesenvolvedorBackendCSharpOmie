ARG DOTNET_VERSION=9.0
ARG BUILD_CONFIGURATION=Release
ARG ASP_NET_RUNTIME=9.0
ARG PROJECT_NAME=OmieVendas.WebApi

# Etapa 1: Configuração do ambiente de desenvolvimento
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /app

# Copia o arquivo de projeto e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copia todos os arquivos do projeto e compila
COPY . ./
RUN dotnet publish -c Release -o out

# Etapa 2: Configuração do ambiente de execução
FROM mcr.microsoft.com/dotnet/aspnet:${ASP_NET_RUNTIME} AS runtime
WORKDIR /app

# Copia os arquivos compilados da etapa anterior
COPY --from=build /app/out .

# Expõe a porta da API
EXPOSE 8080

# dotnet restore
RUN dotnet restore ${PROJECT_NAME}.csproj

# dotnet build
RUN dotnet build ${PROJECT_NAME}.csproj -c ${BUILD_CONFIGURATION} -o /app/build

# dotnet publish
RUN dotnet publish ${PROJECT_NAME}.csproj -c ${BUILD_CONFIGURATION} -o /app/publish -r linux-musl-x64 

FROM mcr.microsoft.com/dotnet/aspnet:${ASP_NET_RUNTIME} AS runtm_omie
WORKDIR /app
COPY --from=build /app/publish .

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "${PROJECT_NAME}.dll"]
