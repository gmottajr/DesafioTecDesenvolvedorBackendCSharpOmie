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

###   Execução Automatizada: 
  - O projeto está **configurado** para iniciar **automaticamente** o SQL Server via Docker Compose toda vez que uma Web API é buildada. 
  - No projeto OmieVendas.WebApi, a seguinte configuração foi adicionada ao arquivo OmieVendas.WebApi.csproj para garantir essa integração:

```xml
  <Target Name="StartDockerCompose" BeforeTargets="Build">
    <Exec Command="docker compose up -d" WorkingDirectory="$(MSBuildProjectDirectory)/../" />
  </Target>
```
### Caseja nessário fazer manualmente

- #### Passo 1: Suba os contêineres com Docker Compose:

  - #####        1.1 (Opcional) Parar e remover contêineres antigos (caso existam conflitos)
    ```bash
    docker-compose down
    ```

    ou, se estiver rodando diretamente:
      ```bash
        docker stop sqlserver_container
        docker rm -f sqlserver_container
      ```

  - ####        1.2 Para Iniciar os contêineres com Docker Compose 

    ```bash
    docker-compose up -d
    
    ```

  - ####        Esse comando iniciará os serviços em segundo plano.
    Verifique se os contêineres estão rodando:
    ```bash
    docker ps
    ```
    Você deverá ver os contêineres webapi_container e sqlserver_container em execução.

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
  - Caso seja necessário, substitua "OmieDb" pelo nome real do seu banco de dados.

### Passo 4: EnsureDatabaseCreated
    
- A solução também inclui um método de extensão EnsureDatabaseCreated no projeto **Omie.IoC**. 
- Esse método verifica a necessidade de criar o banco de dados e rodar migrações automaticamente durante a inicialização da aplicação. 
- Dessa forma a solição garante que o banco de dados **OmieDb** seja criado e as migrações sejam aplicadas.
  
  - ### Aplicar Migrações do Banco de Dados (Se Usando EF Core)
    - Se necessároi, aplique as migrações do banco de dados antes de rodar a API:

      Caso deseje testar as migrações manualmente siga os passos abaixo:
    ```cmd
    dotnet ef database update
    ```

    - Se ainda não houver migrações criadas, gere a primeira:

    ```cmd
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

### Passo 5: Executar a WebAPI

Para iniciar a API, navegue até a pasta raiz do projeto da web API que deseja e execute:

```cmd
dotnet run
```

Estou usando Docker Compose, execute:
```bash
docker-compose up --build
```
## Funcionalidades Gerais

  - Operações CRUD: Cada Web API suporta operações de Criação, Leitura, Atualização e Exclusão para seus respectivos domínios.
  - Integração com Banco de Dados: Utiliza Entity Framework Core para operações ORM com SQL Server.
  - Autenticação e Autorização: Implementa autenticação JWT Bearer para proteger endpoints, com Omie.WebApiAuthorization gerando tokens.
  - Testes Abrangentes: Inclui testes unitários e de integração (e.g., ClienteTests, ProdutoTests, VendasTests) e testes de configuração (ConfigurationLoaderTests).
  - Documentação com Swagger: Oferece documentação interativa para todos os endpoints, com suporte a tokens JWT na UI do Swagger.
    - ***Princípios SOLID:***
      - **Responsabilidade Única:** Cada classe ou método lida com uma funcionalidade específica.
      - **Aberto/Fechado:** O sistema é aberto para extensão, mas fechado para modificação.
      - **Substituição de Liskov:** Classes derivadas podem substituir classes base sem alterar a funcionalidade.
      - **Segregação de Interface:** Clientes não dependem de interfaces que não utilizam.
      - **Inversão de Dependência:** Módulos de alto nível dependem de abstrações, não de implementações.
      Web APIs Específicas

  - **Omie.WebApiVendas:** Gerencia operações relacionadas a vendas via endpoints como /api/Vendas.
  - **Omie.WebApiClientes:** Gerencia dados de clientes via endpoints como /api/Clientes. 
  - **Omie.WebApiProdutos:** Gerencia dados de produtos via endpoints como /api/Produtos. 
  - **Omie.WebApiAuthorization:** Gerencia autenticação e geração de tokens JWT via /api/authenticate. 
  - **Todas as Web APIs** herdam de ****Omie.Common.Abstractions.Controllers.OmieCrudBaseController**** e utilizam abstrações como **IAppServiceBase**, **IDataRepositoryBase**, e **IResourceDtoBase** de **Omie.Common.Abstractions**.

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

### Passo 8: Estratégia para Obter um Token JWT Usando Omie.WebApiAuthorization

Para autenticar e obter um token JWT para acessar endpoints protegidos , siga estas etapas:

- #### Execute o projeto ```OmieAuthentication.WebApi```
  - em seguinda access: ```http://localhost:5030```
  - Endpoint: Use o endpoint ```/api/Authentic/uthenticating``` em Omie.WebApiAuthorization para solicitar um token. 
    - Uma requisição POST com credenciais. 
    - O token é um JWT que **NÃO** pode ser usado para autenticar requisições subsequentes às outras Web APIs.

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


## OmieClientes.WebApi
Bem-vindo ao **OmieClientes.WebApi**, um microsserviço projetado para gerenciar operações relacionadas aos clientes dentro do ecossistema Omie. Este serviço segue os princípios SOLID para garantir manutenibilidade, escalabilidade e separação de preocupações.
###   Funcionalidades
- **Operações CRUD para Clientes**:
  - Operações de Criação, Leitura, Atualização e Exclusão de dados de clientes.
- **Integração com Banco de Dados**:
  - Utiliza Entity Framework Core para operações ORM com SQL Server.
- **Autenticação e Autorização**:
  - Autenticação JWT Bearer implementada para proteger endpoints.
  - Um endpoint para gerar tokens JWT para fins de autenticação.
- **Testes Abrangentes**:
  - Testes unitários e de integração para operações CRUD (`ClienteTests`, `ProdutoTests`, `VendasTests`).
  - Testes de configuração (`ConfigurationLoaderTests`) para garantir o carregamento correto da configuração da aplicação.
- **Documentação de API com Swagger**:
  - Documentação interativa para todos os endpoints.
  - Configuração de segurança na UI do Swagger para lidar com tokens JWT.
- **Princípios SOLID**:
  - **Responsabilidade Única**: Cada classe ou método lida com uma parte da funcionalidade.
  - **Aberto/Fechado**: O sistema é aberto para extensão, mas fechado para modificação.
  - **Substituição de Liskov**: Classes derivadas podem substituir classes base sem alterar a funcionalidade.
  - **Segregação de Interface**: Clientes não são forçados a depender de interfaces que não usam.
  - **Inversão de Dependência**: Módulos de alto nível não dependem de módulos de baixo nível, ambos dependem de abstrações.


