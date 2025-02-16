# Desafio Tecnico Desenvolvedor Backend C# Omie
Descrição do Cenário Você foi contratado para criar uma API que suporte as funcionalidades apresentadas no mockup de tela fornecido em anexo. A API deve ser capaz de gerenciar e processar as informações exibidas na interface, permitindo que elas sejam persistidas de forma consistente.  

## Executando a WebAPI Backend
Este guia fornece instruções detalhadas para executar a WebAPI e garantir que o banco de dados SQL Server em Docker esteja disponível e configurado corretamente.

### Pré-requisitos

Antes de começar, certifique-se de ter os seguintes requisitos instalados:

- [Docker](https://docs.docker.com/get-docker/)
- [Docker Compose](https://docs.docker.com/compose/install/)
- [.NET SDK 9](https://dotnet.microsoft.com/en-us/download/dotnet)
- [(Opcional) Postman ou outra ferramenta para testar APIs](https://www.postman.com/downloads/)

## Configuração e Inicialização dos Contêineres

### Passo 1: Suba os contêineres com Docker Compose:

####        1.1 (Opcional) Parar e remover contêineres antigos (caso existam conflitos)
```bash
docker-compose down
```

ou, se estiver rodando diretamente:
```bash
docker stop sqlserver_container
docker rm -f sqlserver_container
```

####        1.2 Iniciar os contêineres com Docker Compose (mais recomendado)
```bash
docker-compose up -d
```

####        Esse comando iniciará os serviços em segundo plano.
Verifique se os contêineres estão rodando:
```bash
docker ps
```
Você deverá ver os contêineres webapi_container e sqlserver_container em execução.

### Passo 2: Iniciar o SQL Server no Docker
Execute o seguinte comando para criar e iniciar um container do SQL Server 2022:

####    Para Linux/macOS (Usando Barra Invertida para Quebra de Linha)
```sh
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=My#Stron8P4ssw0rd" \
  -p 1433:1433 --name sqlserver_container -d mcr.microsoft.com/mssql/server:2022-latest
```
    Certifique-se de que não há espaços após a \ no final da linha.

####    Para Windows PowerShell
```powershell
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=My#Stron8P4ssw0rd" `
  -p 1433:1433 --name sqlserver_container -d mcr.microsoft.com/mssql/server:2022-latest
```
    No PowerShell, use o acento grave (`) em vez da barra invertida '\' para comandos em várias linhas.

####    Para Windows CMD (Rodar em uma Linha Única)
```cmd
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=My#Stron8P4ssw0rd" -p 1433:1433 --name sqlserver_container -d mcr.microsoft.com/mssql/server:2022-latest
```
O CMD do Windows não suporta comandos multi-linhas com \, então rode tudo em uma única linha.

#####           Conflito com um Contêiner Existente:
Se aparecer um erro dizendo que o nome sqlserver_container já está em uso, remova o contêiner antigo antes de criar um novo:

```cmd    
docker rm -f sqlserver_container
```

#### Observações:
    - A senha (SA_PASSWORD) deve ser a mesma que vc definir no docker-compose.yml.
    O container irá rodar o SQL Server na porta 1433 do seu computador.
    Para verificar se o container está em execução:

```bash
docker ps
```
Se o container estiver parado, reinicie-o com:

```bash
docker start sqlserver_container
```

Para verificar os logs do SQL Server no Docker:

```bash
docker logs sqlserver_container
```

### Passo 3: Configurar a String de Conexão

Modifique o arquivo appsettings.json (ou appsettings.Development.json) do seu projeto para conectar-se ao SQL Server no Docker:
Por exemplo, eu defini o appsetting corrente como:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=sqlserver,1433;Database=OmieDb;User Id=sa;Password=My#Stron8P4ssw0rd;TrustServerCertificate=True;"
}
```

##### Nota:
    - O parâmetro TrustServerCertificate=True é necessário para ambientes locais.
Caso seja necessario, substitua "OmieDb" pelo nome real do seu banco de dados.

### Passo 4: Aplicar Migrações do Banco de Dados (Se Usando EF Core)

Caso seu projeto utilize Entity Framework Core, aplique as migrações do banco de dados antes de rodar a API:

```cmd
dotnet ef database update
```

    - Se ainda não houver migrações criadas, gere a primeira:

```cmd
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Passo 5: Executar a WebAPI

Para iniciar a API, navegue até a pasta raiz do projeto e execute:

```cmd
dotnet run
```

Estou usando Docker Compose, execute:
```bash
docker-compose up --build
```

### Passo 6: Testar a API

Após iniciar a WebAPI, você pode testá-la utilizando cURL, Postman ou diretamente no navegador.

Se o Swagger estiver habilitado, acesse:
```html
http://localhost:5000/swagger
```

Se estiver rodando em HTTPS, acesse:
```cpp
https://localhost:5001/swagger
```
### Passo 7: Parar e Remover o Container do SQL Server (Se Necessário)

Para parar o container do SQL Server sem removê-lo:
```cpp
docker stop sqlserver_container
```

Para remover completamente o container:
```cpp
docker rm -f sqlserver_container
```

### Solução de Problemas

    ❌ Erro de conexão com o banco de dados
        Verifique se o container do SQL Server está rodando:
    
```
docker ps
```
        - Certifique-se de que a porta 1433 está correta na appsettings.json.
Em Mac/Linux, se houver problemas com localhost, tente usar:
    ```
        Server=host.docker.internal,1433;
    ```

    - ❌ Erro de porta em uso
        - Se a porta 1433 já estiver ocupada, descubra o processo em execução:
```cpp
sudo lsof -i :1433
kill -9 <PID>
```

        - Ou inicie o container em outra porta:
```cpp
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=SuaSenhaForte!123" \
  -p 1434:1433 --name sqlserver_container -d mcr.microsoft.com/mssql/server:2022-latest
E atualize a appsettings.json:
```

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1434;Database=SeuBancoDeDados;User Id=sa;Password=SuaSenhaForte!123;TrustServerCertificate=True"
}
```


