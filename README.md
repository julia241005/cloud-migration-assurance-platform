# Cloud Migration Assurance Platform

A Cloud Migration Assurance Platform (CMA) é uma plataforma desenvolvida para apoiar a validação de aplicações após processos de migração de infraestrutura para ambientes de nuvem.

O objetivo do projeto é garantir que aplicações e seus componentes essenciais continuem operacionais após a migração, reduzindo riscos de indisponibilidade, falhas de comunicação, problemas de configuração e impactos no funcionamento da aplicação.

A plataforma foi concebida para executar verificações sobre workloads migrados e fornecer evidências sobre o estado do ambiente após a migração.

---

## Objetivo

Durante uma migração de infraestrutura, mover uma aplicação de um ambiente para outro não significa que ela continuará funcionando corretamente.

Após a migração, podem surgir problemas relacionados a:

- conectividade de rede;
- DNS;
- portas e regras de firewall;
- certificados e HTTPS;
- servidores de aplicação;
- IIS;
- acesso ao banco de dados;
- configurações da aplicação;
- disponibilidade do workload.

O CMA tem como objetivo apoiar a etapa de **Migration Assurance**, realizando verificações pós-migração para identificar esses problemas antes que eles afetem a operação.

### Fluxo conceitual

```text
Migração
   ↓
Workload migrado
   ↓
Validações pós-migração
   ↓
Verificação dos componentes
   ↓
Resultados dos checks
   ↓
Identificação de falhas ou riscos
   ↓
Evidências para validação do ambiente
O que é um Workload?

No contexto da plataforma, um workload representa uma aplicação, sistema ou conjunto de recursos que passou por um processo de migração e precisa ser validado no ambiente de destino.

Cada workload pode possuir diferentes verificações relacionadas à sua operação.

Exemplos:

disponibilidade da aplicação;
resolução DNS;
conectividade;
HTTPS;
servidor de aplicação;
banco de dados;
serviços necessários para funcionamento.
Arquitetura

O projeto utiliza uma arquitetura organizada em camadas, separando responsabilidades entre API, aplicação, domínio e infraestrutura.

CMA.Api
   ↓
CMA.Application
   ↓
CMA.Domain
   ↓
CMA.Infrastructure
CMA.Api

Responsável pela exposição dos endpoints HTTP e pela comunicação com o cliente.

Tecnologias e componentes:

ASP.NET Core
REST API
Controllers
Swagger / OpenAPI
Dependency Injection
CMA.Application

Contém os casos de uso da aplicação.

Atualmente possui operações relacionadas a:

criação de workloads;
consulta de workloads;
consulta de workload por ID;
execução de verificações.
CMA.Domain

Contém as regras e entidades centrais do domínio.

Entre elas:

Workload
VerificationCheck
VerificationStatus
CMA.Infrastructure

Responsável pelas implementações relacionadas à infraestrutura da aplicação.

Atualmente possui:

IWorkloadRepository
InMemoryWorkloadRepository

A persistência em memória faz parte da versão inicial do projeto e será substituída por uma implementação de persistência real durante a evolução da plataforma.

Tecnologias
C#
.NET
ASP.NET Core
REST API
Swagger / OpenAPI
HTML
JavaScript
Dependency Injection
Repository Pattern
Git
GitHub
Estado atual

O projeto encontra-se em desenvolvimento.

Implementado
Estrutura inicial da plataforma;
API ASP.NET Core;
arquitetura em camadas;
cadastro de workloads;
listagem de workloads;
consulta de workload;
casos de uso separados;
Repository Pattern;
injeção de dependência;
frontend inicial;
Swagger / OpenAPI;
modelo inicial de verificações;
controle de status das verificações.
Em evolução
Persistência em banco de dados;
execução real dos checks de infraestrutura;
validações de DNS;
validações HTTP/HTTPS;
validações de disponibilidade;
validações de conectividade;
validações relacionadas ao IIS;
validações de banco de dados;
armazenamento dos resultados das verificações;
histórico das execuções;
dashboard de acompanhamento;
automação das validações pós-migração.
Como executar o projeto
Pré-requisitos

Antes de executar o projeto, é necessário possuir:

.NET SDK instalado;
Git;
Visual Studio ou outro ambiente compatível com .NET.

Verifique a instalação do .NET:

dotnet --version
Clonar o repositório
git clone https://github.com/julia241005/cloud-migration-assurance-platform.git

Entre na pasta:

cd cloud-migration-assurance-platform
Restaurar as dependências

Execute:

dotnet restore
Compilar a solução
dotnet build
Executar a API

Entre na pasta da API:

cd src/CMA.Api/CMA.Api

Execute:

dotnet run

A API será iniciada em uma URL local exibida no terminal.

No ambiente de desenvolvimento, o Swagger pode ser acessado pela rota:

/swagger
Estrutura do projeto
cloud-migration-assurance-platform/
│
├── docs/
│
├── infra/
│
├── src/
│   │
│   ├── CMA.Api/
│   │   └── CMA.Api/
│   │
│   ├── CMA.Application/
│   │
│   ├── CMA.Domain/
│   │
│   └── CMA.Infrastructure/
│
├── .gitignore
├── CloudMigrationAssurance.slnx
└── README.md
Exemplo de fluxo da aplicação

Um workload pode ser cadastrado na plataforma e posteriormente consultado para execução ou acompanhamento de verificações.

Cadastrar Workload
        ↓
Identificar recursos
        ↓
Executar verificações
        ↓
Registrar resultados
        ↓
Analisar status

Os resultados das verificações são representados por estados como:

OK
WARNING
FAIL
TIMEOUT
NOT_TESTED
UNKNOWN
Visão futura

A evolução da plataforma busca aproximar o CMA de uma ferramenta de validação automatizada para ambientes de Cloud Migration.

A visão é permitir que, após a migração de um workload, a plataforma consiga validar automaticamente os principais pontos necessários para sua operação.

                    CLOUD MIGRATION
                           │
                           ▼
                    WORKLOAD MIGRADO
                           │
                           ▼
                 CMA - ASSURANCE LAYER
                           │
          ┌────────────────┼────────────────┐
          ▼                ▼                ▼
         DNS            HTTPS/IIS        DATABASE
          │                │                │
          └────────────────┼────────────────┘
                           ▼
                   VALIDATION ENGINE
                           │
                           ▼
                    RESULTADOS
                           │
              ┌────────────┼────────────┐
              ▼            ▼            ▼
             OK         WARNING        FAIL

O objetivo é transformar essas validações em um processo estruturado, rastreável e automatizado, reduzindo o risco de problemas após a migração.

Contexto

O projeto foi desenvolvido a partir de conceitos relacionados a:

Cloud Computing;
Infraestrutura;
Azure;
Cloud Migration;
DNS;
redes;
servidores de aplicação;
IIS;
bancos de dados;
monitoramento;
validação pós-migração.
Status do projeto

Em desenvolvimento

A versão atual representa a base arquitetural da plataforma. Novas funcionalidades serão adicionadas progressivamente, principalmente relacionadas à persistência de dados e à execução automatizada das verificações de infraestrutura.


### E eu mudaria uma coisa importante na apresentação do projeto

Não colocaria o CMA como:

> "um sistema que cadastra workloads"

Isso descreve **a implementação atual**, mas não **o problema que o projeto resolve**.

A apresentação correta é:

> **CMA é uma plataforma de Cloud Migration Assurance desenvolvida para validar workloads após uma migração de infraestrutura, verificando se os componentes necessários para o funcionamento da aplicação permanecem operacionais e identificando possíveis falhas ou impactos no ambiente pós-migração.**

Isso conversa diretamente com aquilo que você aprendeu no handover: **migrou, mas agora precisa provar que a aplicação continua funcionando**.

E tem uma evolução muito boa para fazermos depois: o `InMemoryWorkloadRepository` que temos hoje é justamente o ponto em que
