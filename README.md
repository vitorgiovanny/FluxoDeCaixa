

# Documentação em Construção

### Aplicação Fluxo de Caixa

## Indice

- [Introdução](#Introducao)
- [Fluxo da Solução](#fluxo-da-Solução)
- [Fluxo de Negocio](#Fluxo-de-Negocio)
- [Arquitetura do Projeto](#Arquitetura-do-Projeto)
- [Instrução](#Instrucao)
- [Futuras Melhorias](#Futuras-Melhorias)
- [Swagger](#Swagger)

## Introducao
Essa aplicação representa o **core domain** do nosso sistema, concentrando a lógica mais valiosa e complexa do nosso negócio de fluxo de caixa diário. Cada comerciante terá um caixa individual para registrar entradas e saídas de dinheiro, garantindo um controle financeiro preciso. Além disso, a aplicação gerará relatórios diários detalhados, oferecendo uma visão clara e organizada das transações realizadas.

### Descritivo da Solução
Um comerciante precisa controlar o seu fluxo de caixa diário com os lançamentos (débitos e créditos), também precisa de um relatório que disponibilize o saldo
 diário consolidado.
## Fluxo da Solucão
Seguindo a arquitetura de Microsserviços, separamos os serviços da seguinte forma(segue imagem abaixo):
![Fluxo da separação dos serviços](Img/fluxoServicos.png)

| Serviço           | Responsabilidade                                                 |
|------------------|-----------------------------------------------------------------|
| **API Credit**   | Adicionar dinheiro ao caixa                                    |
| **API Debit**    | Remover (debitar) dinheiro do caixa                            |
| **API Cash Balance** | Gerenciar o caixa geral, fornecer extratos, relatórios e cadastrar comerciantes |
| **API Gateway**  | Processar, monitorar e controlar as chamadas de API           |

## Fluxo de Negocio
![fluxo de negocio](Img/FluxoNegocio.png)
## Arquitetura do Projeto
Seguindo arquitetura DDD, e pensando na escabilidade horizontal(scale-out) e na distribuição de carga para multiplos serviços foi usado o Microsserviços.

### DDD

A nossa arquitetura segue os princípios do Domain-Driven Design (DDD) organizando a solução em diferentes contextos delimitados (Bounded Contexts). Cada API representa *um contexto específico*, garantindo separação de responsabilidades e independência entre os domínios.

No DDD o ideal é que o repositório seja específico para cada agregado raiz, ou seja, Isso significa que repositórios **não devem ser genéricos em excesso**, mas **um repositório genérico pode ser útil internamente para evitar repetição de código**.

No nosso projeto, o repositório genérico é suficiente para atender nossas necessidades no momento. Embora Cash só possa existir se o Cashier (comerciante) existir, isso torna o Cashier o Aggregate Root, enquanto Cash é uma entidade pertencente ao agregado de Cashier. Dessa forma, qualquer movimentação de saldo ocorre através do Cashier.

Por outro lado, Extract é um Aggregate Root independente, pois, apesar de precisar do Cash para existir, ele é manipulado de forma autônoma. Quando ocorre uma transação de débito ou crédito, uma mensagem é enviada para a API CashBalance, que registra a transação no banco de dados, garantindo a independência do seu ciclo de vida.

A abordagem de repositório genérico foi mantida para todas as entidades por enquanto, até que seja necessária uma melhoria futura para separar os repositórios conforme o crescimento da aplicação.

**Confira no topico Futuras Melhorias** para verificar quais proximos passos.

### Menssageria
O tipo de Exchange utilizado no RabbitMQ foi o **Direct Exchange**, pois apenas o serviço APIBALANCE precisa escutar/receber as mensagens. Esse tipo de roteamento garante uma entrega direta e previsível, sem a necessidade de broadcast. Além disso, a routing key foi utilizada para indicar se a operação foi de crédito ou débito.

Diretorio 
```
Api Fluxo de Caixa
├── ApiBalance.Test #Testes automatizados
├── ApiCashBalance #Camada de Apresentação (Controllers, Endpoints)(Api)
├── CashBalance.Application #Camada de Aplicação (Orquestração, DTOs, Use Cases)
├── CashBalance.Domain #Camada de Domínio (Entidades, Regras de Negócio, Interfaces)
└── CashBalance.Infrastructure #Camada de Infraestrutura (Repositórios, Banco, Serviços Externos)

Api Credito
├───ApiCredit.Test #Testes automatizados
├───ApiCredit #Camada de Apresentação (Controllers, Endpoints)(Api)
├───ApiCredit.Application #Camada de Aplicação (Orquestração, DTOs, Use Cases)
├───ApiCredit.Domain #Camada de Domínio (Entidades, Regras de Negócio, Interfaces)
└───ApiCredit.Infrastructure #Camada de Infraestrutura (Repositórios, Banco, Serviços Externos)

Api Debit
├───ApiDebit.Test #Testes automatizados
├───ApiDebit #Camada de Apresentação (Controllers, Endpoints)(Api)
├───ApiDebit.Application #Camada de Aplicação (Orquestração, DTOs, Use Cases)
├───ApiDebit.Domain #Camada de Domínio (Entidades, Regras de Negócio, Interfaces)
└───ApiDebit.Infrastructure #Camada de Infraestrutura (Repositórios, Banco, Serviços Externos)
```

## Instrução

### Pré-requisito
Antes de rodar o projeto, certifique-se de ter instalado:

```.NET SDK 8.0```

```Docker```

## Rodando a Aplicação

### 📌 Configuração do RabbitMQ  

Utilizamos a imagem oficial do **RabbitMQ** com painel de gerenciamento integrado.  

###  Baixar a imagem oficial do RabbitMQ  
```bash
docker pull rabbitmq:3-management
```

### Baixar a imagem oficial do RabbitMQ  
```bash
docker pull rabbitmq:3-management 
```
###  Subir um container RabbitMQ
```bash 
docker run --rm  -it -p 15672:15672 -p 5672:5672 rabbitmq:3-management
```

### ⚙️ Configuração do .NET

**Appsettings Connection String, fazer isso nas APIS(API CREDIT, API DEBIT, APICASHBALANCE)**
```bash
 },
  "ConnectionStrings": {
    "DefaultConnection": "{SQL SERVER}"
  }
```
Exemplo de connection string
```bash
Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;
```

**Aplicar as migrações**
Na camada/projeto Infrastructure do ApiCashBalance coloca esse comando:

```bash
update-database
```
**Restaure as dependências**
```bash
dotnet restore
```

### Rodar as APIs

```bash
cd {Nome da API (CashBalance, ApiCredit, ApiDebit, ApiGateway)}
dotnet run
```

### Rodar os testes das Apis

```bash
cd {Nome da API}.Test
dotnet test
```
### 📌 API Gateway Endpoints

#### Comerciante 
**Endpoint:**  
`POST /gateway/cashier/creater`  

**Descrição:**  
Cria um novo **comerciante**.  

**Parametros**
```bash
querystring:cashierName
```
```bash
?cashierName=nome
```
---

#### Listar Todos os Comerciantes
**Endpoint:**  
`GET /gateway/cashier/GetAll`  

**Descrição:**  
Retorna todos os **comerciantes** cadastrados.  

---

#### Obter Extrato Geral
**Endpoint:**  
`GET /gateway/extract/getextract`  

**Descrição:**  
Retorna o extrato financeiro de um **caixa**.  

---

#### Obter Relatório Diario
**Endpoint:**  
`GET /gateway/extract/getreport`  

**Descrição:**  
Retorna um relatório financeiro detalhado de um **Caixa**.  

#### Adicionar Credito no caixa
**Endpoint:**  
`POST /gateway/Cash/AddCredit`

**Parametros**
```bash
{
  "Amount": 100.50,
  "IdCashed": "d3b07384-d113-4f3e-a875-fbfcf1ff89a6",
  "IdCash": "f47ac10b-58cc-4372-a567-0e02b2c3d479"
}
```

**Descrição:**  
Retorna um relatório financeiro detalhado de um **Caixa**.  


## Futuras Melhorias

*Aggregate Root*

Atualmente, utilizamos um repositório genérico para atender nossas necessidades. No entanto, para alinhar melhor com os princípios do DDD, é necessário refatorar os repositórios considerando os Aggregate Roots. A correção envolve manter repositórios específicos apenas para Cashier e Extract, garantindo que entidades dependentes sejam manipuladas exclusivamente por seus agregados.

## Swagger
```
localhost:5000/swagger
```