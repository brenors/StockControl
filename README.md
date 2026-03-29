StockControl API

API desenvolvida em .NET para controle de estoque e gerenciamento de produtos e pedidos, seguindo os princípios de Clean Architecture.
Este projeto foi construído com foco em boas práticas de engenharia de software, organização de código, testabilidade e simulação de um ambiente real de produção.

A aplicação permite:

Cadastro e autenticação de usuários
Gerenciamento de produtos
Controle de estoque com rastreabilidade (nota fiscal)
Emissão de pedidos com validação de estoque

Tecnologias utilizadas:
- .NET 9
- Entity Framework Core
- SQL Server (Docker)
- JWT Authentication
- Swagger (OpenAPI)
- AutoMapper
- xUnit + Moq + FluentAssertions
- OpenTelemetry (Tracing/APM)


<h3>Arquitetura<h3>

O projeto segue o padrão Clean Architecture, dividido em camadas:

src/
 ├── StockControl.API            → Controllers, Middlewares, Configuração
 ├── StockControl.Application    → Serviços, DTOs, Regras de negócio
 ├── StockControl.Domain         → Entidades, Enums, Validações
 └── StockControl.Infrastructure → Repositórios, DbContext, Migrations
tests/
 └── StockControl.Tests          → Testes unitários
 
Princípios aplicados:
- Separação de responsabilidades
- Inversão de dependência
- Baixo acoplamento
- Alta coesão
- Testabilidade

  
Pacotes por projeto:
StockControl.API
- Microsoft.AspNetCore.Authentication.JwtBearer
- Swashbuckle.AspNetCore
- OpenTelemetry.Extensions.Hosting
- OpenTelemetry.Instrumentation.AspNetCore
- OpenTelemetry.Exporter.Console
  
StockControl.Application
- AutoMapper
- StockControl.Infrastructure
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Design

StockControl.Tests
- xUnit
- Moq
- FluentAssertions

  
Como rodar o projeto
1. Subir banco de dados (Docker)

Na raiz do projeto:

docker-compose up -d

Isso irá subir um container com SQL Server.

2. Criar migration (caso necessário)

A partir da raiz do projeto:

cd src
dotnet ef migrations add InitialCreate \
--project StockControl.Infrastructure \
--startup-project StockControl.API
3. Aplicar migration no banco
dotnet ef database update \
--project StockControl.Infrastructure \
--startup-project StockControl.API
4. Rodar a API
cd src/StockControl.API
dotnet run
5. Acessar Swagger
https://localhost:{porta}/swagger
🔐 Autenticação

A API utiliza autenticação via JWT.

Como utilizar no Swagger:
Faça login
Copie o token retornado
Clique em Authorize
Insira:
Bearer {seu_token}
🧪 Fluxo completo da aplicação (passo a passo)
🔹 1. Cadastro de usuário

POST /api/auth/register

{
  "name": "Admin",
  "email": "admin@test.com",
  "password": "123456",
  "role": "Admin"
}

📌 Regras:

Email deve ser único
Senha mínima de 6 caracteres
Role: Admin ou Seller
🔹 2. Login

POST /api/auth/login

{
  "email": "admin@test.com",
  "password": "123456"
}

📌 Retorno:

Token JWT
🔹 3. Criar produto (Admin)

POST /api/products

{
  "name": "Bola de Futebol",
  "description": "Bola profissional",
  "price": 150
}
🔹 4. Listar e filtrar produtos

GET /api/products?name=bola&minPrice=100&maxPrice=200

🔹 5. Adicionar estoque (Admin)

POST /api/stocks

{
  "productId": "GUID_DO_PRODUTO",
  "quantity": 10,
  "invoiceNumber": "NF-123"
}

📌 Regras:

Produto deve existir
Nota fiscal obrigatória
🔹 6. Consultar estoque disponível

GET /api/stocks/{productId}/available

🔹 7. Criar pedido (Seller)

POST /api/orders

{
  "customerDocument": "12345678900",
  "sellerName": "João",
  "items": [
    {
      "productId": "GUID_DO_PRODUTO",
      "quantity": 2
    }]
}

📌 Regras:

Produto deve existir
Deve haver estoque suficiente
Caso contrário → erro
Estoque é reduzido automaticamente
🧪 Testes unitários

Executar testes:

cd tests/StockControl.Tests
dotnet test
✔ Cobertura de testes
AuthService
ProductService
StockService
OrderService
📊 Observabilidade (Tracing/APM)

A aplicação utiliza OpenTelemetry para monitoramento:

Requisições HTTP
Tempo de execução
Logs no console

📌 Configurado em:

StockControl.API
⚠️ Regras de negócio implementadas
Usuário com role (Admin ou Seller)
Apenas Admin pode:
Criar produtos
Gerenciar estoque
Pedido só é criado com estoque disponível
Baixa automática de estoque
Registro de entrada de estoque com nota fiscal
💎 Diferenciais implementados
Clean Architecture
JWT Authentication com Roles
Testes unitários
Swagger com autenticação
OpenTelemetry (Tracing)
Docker para banco de dados
📌 Considerações técnicas
Persistência feita com EF Core
Padrão Repository aplicado
Separação clara entre camadas
Mapeamento com AutoMapper
Validações centralizadas
🏁 Conclusão

O projeto foi desenvolvido visando:

Simular ambiente real de produção
Garantir organização e escalabilidade
Facilitar manutenção e testes
Aplicar boas práticas de engenharia de software
