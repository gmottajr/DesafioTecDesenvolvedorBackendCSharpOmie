# Desafio Tecnico Desenvolvedor Backend C# Omie
Descrição do Cenário Você foi contratado para criar uma API que suporte as funcionalidades apresentadas no mockup de tela fornecido em anexo. A API deve ser capaz de gerenciar e processar as informações exibidas na interface, permitindo que elas sejam persistidas de forma consistente.  

## Executando a WebAPI Backend
Este guia fornece instruções detalhadas para executar a WebAPI e garantir que o banco de dados SQL Server em Docker esteja disponível e configurado corretamente.

### Pré-requisitos

Certifique-se de ter os seguintes softwares instalados:

    - Docker
    - .NET SDK 9
    - (Opcional) Postman ou outra ferramenta para testar APIs

### Passo 1: Iniciar o SQL Server no Docker
Execute o seguinte comando para criar e iniciar um container do SQL Server 2022:
    - ```
    docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<Senha definida no docker-compose.yml>" \ -p 1433:1433 --name sqlserver_container -d mcr.microsoft.com/mssql/server:2022-latest
    ```

