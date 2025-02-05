# Projeto de Visualização de Dados Alimentares

Este é um aplicativo responsável pela visualização de dados alimentares, incluindo funcionalidades para acessar, atualizar e armazenar informações no banco de dados SQLite. O projeto também possui um scraper automatizado para extração de dados alimentares de fontes externas.

## Tecnologias Utilizadas

- C#
- ASP.NET Core
- SQLite
- HtmlAgilityPack (para scraping)
- React (TypeScript)
- Material UI
- Docker Compose

## Estrutura do Projeto

### Backend:

- **Controllers:**

  - `FoodController`: Gerencia operações de alimentos.
    - `GetFoods()`: Retorna todos os alimentos do banco de dados.
    - `GetFoodDetails(string foodCode)`: Retorna os detalhes de um alimento específico.
  - `ScraperController`: Gerencia operações de scraping.
    - `UpdateFoodsAsync()`: Atualiza os alimentos a partir de extração automatizada.

- **Data:**

  - `DatabaseInitializer`: Inicializa o banco e cria as tabelas.
    - `Initialize()`: Cria as tabelas "Food" e "FoodDetails" se não existirem.
  - **Repository**: Responsável pela interação com o banco de dados.
    - `UpdateFoods(IEnumerable<Food>)`: Atualiza alimentos no banco.
    - `SaveFoodDetails(List<FoodDetails>)`: Salva os detalhes dos alimentos.
    - `GetAllFoods()`: Retorna todos os alimentos.
    - `GetFoodDetailsByCode(string foodCode)`: Retorna detalhes de um alimento pelo código.

- **Models:**

  - `Food`: Representa um alimento.
  - `FoodDetails`: Detalhes nutricionais de um alimento.

- **Services:**

  - `Scraper`: Realiza scraping dos dados alimentares.
    - `ScrapeFoodsAsync()`: Obtém a lista de alimentos e armazena no banco.
    - `GetFoodDetailsAsync(HttpClient client, string foodCode)`: Obtém detalhes de um alimento.

- **Program**:
  - Configura e inicializa o backend.

### Frontend:

- **Tecnologias:**

  - React (TypeScript)
  - Material UI

  A aplicação exibe uma interface com duas páginas principais:

  - **Página Inicial**: Exibe uma tabela com todos os alimentos e um botão para atualizar os dados. O botão aciona um processo de scraping que demora cerca de 5 minutos para buscar e gravar os dados no banco de dados SQLite. Após a atualização, os dados são persistidos no SQLite e a lista de alimentos é automaticamente carregada na tabela.

  - **Página de Detalhes**: Ao clicar em um alimento na tabela, o usuário é redirecionado para uma nova página onde são exibidos os detalhes nutricionais do alimento. Há um botão de voltar que redireciona para a página anterior.

### Docker Compose:

- O backend e o frontend são containerizados usando Docker Compose.
- O banco de dados SQLite é persistido usando volumes Docker.

## Passos para Configuração e Execução

### 1. Clone o repositório:

Primeiro, faça o clone do repositório utilizando o seguinte comando:

```bash
git clone https://github.com/Leandro-Lucena/food-info-scraper.git
cd projeto-alimentos
```

### 2. Configure o .env no frontend:

No diretório do frontend, renomeie o arquivo .env.example para .env.

```bash
cp .env.example .env
```

### 3. Requisitos para Rodar o Docker Compose:

- **Windows:**

  - Certifique-se de ter o Docker Desktop instalado.
  - Ative a virtualização no BIOS e habilite o WSL2 (Windows Subsystem for Linux 2) no Docker Desktop.

- **Linux/Mac:**
  - Tenha o Docker Engine e o Docker Compose instalados.
  - Verifique se você tem permissões para rodar o Docker (em Linux, pode ser necessário usar `sudo` para comandos do Docker).

### 4. Suba os containers com Docker Compose:

No diretório raiz do projeto, execute o seguinte comando:

```bash
docker-compose up
```

Esse comando irá construir e iniciar os containers do backend e frontend. O servidor estará disponível em http://localhost:3000.

##

A estrutura foi cuidadosamente organizada para fornecer uma solução escalável e de fácil manutenção, com foco na simplicidade e no uso de boas práticas de desenvolvimento. Fico à disposição para esclarecer dúvidas ou fornecer mais informações sobre o projeto.
