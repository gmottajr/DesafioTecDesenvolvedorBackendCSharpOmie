ARG DOTNET_VERSION=9.0
ARG BUILD_CONFIGURATION=Release
ARG ASP_NET_RUNTIME=9.0
ARG PROJECT_NAME=OmieVendas.WebApi

# Etapa 1: Configuração do ambiente de desenvolvimento
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /app

# Copia o arquivo de solução e os arquivos de projeto e restaura as dependências
COPY *.sln .
COPY src/Omie.Application/*.csproj ./src/Omie.Application/
COPY src/Omie.DAL/*.csproj ./src/Omie.DAL/
COPY src/Omie.Domain/*.csproj ./src/Omie.Domain/
COPY src/Omie.Ioc/*.csproj ./src/Omie.Ioc/
COPY src/Omie.WebApi/*.csproj ./src/Omie.WebApi/
COPY tests/Database.Tests/*.csproj ./tests/Database.Tests/
COPY tests/Tests.Common/*.csproj ./tests/Tests.Common/
COPY tests/Utilities.Tests/*.csproj ./tests/Utilities.Tests/

RUN dotnet restore

# Copia todos os arquivos do projeto e compila
COPY . ./
RUN dotnet publish -c ${BUILD_CONFIGURATION} -o out

# Etapa 2: Configuração do ambiente de execução
FROM mcr.microsoft.com/dotnet/aspnet:${ASP_NET_RUNTIME} AS runtime
WORKDIR /app

# Copia os arquivos compilados da etapa anterior
COPY --from=build /app/out .

# Expõe a porta da API
EXPOSE 8080

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "${PROJECT_NAME}.dll"]