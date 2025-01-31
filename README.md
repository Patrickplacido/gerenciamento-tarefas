# MyTaskManager

MyTaskManager é um sistema de gerenciamento de tarefas pessoais desenvolvido em **ASP.NET Core 8.0** com **Entity Framework Core 8.0** e **SQL Server**. Ele permite que os usuários criem, editem, excluam e visualizem tarefas, além de filtrá-las por status e data de vencimento. O sistema utiliza autenticação baseada em **JWT (JSON Web Tokens)** para garantir a segurança das operações.

## Pré-requisitos

Antes de começar, certifique-se de que você tem os seguintes itens instalados:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (ou [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads))
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (ou [Visual Studio Code](https://code.visualstudio.com/))

## Como Configurar o Projeto

### 1. Clone o Repositório

Clone o repositório do projeto para o seu ambiente local:

```bash
git clone https://github.com/seu-usuario/MyTaskManager.git
cd MyTaskManager
```

### 2. Configure o Banco de Dados
Abra o arquivo appsettings.json e configure a string de conexão do SQL Server:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=MyTaskManagerDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

Substitua SEU_SERVIDOR pelo nome do seu servidor SQL Server (por exemplo, localhost ou (localdb)\\mssqllocaldb).

Execute as migrations para criar o banco de dados e as tabelas necessárias:

```bash
dotnet ef database update
```

Isso aplicará as migrations e criará o banco de dados MyTaskManagerDb no SQL Server.

### 3. Configure o JWT (Autenticação)
No arquivo appsettings.json, configure as opções do JWT:

```json
"JwtSettings": {
  "SecretKey": "sua_chave_secreta_super_segura_aqui"
}
```

Substitua sua_chave_secreta_super_segura_aqui por uma chave segura.

### 4. Execute o Projeto
No terminal, navegue até a pasta do projeto e execute:

```bash
dotnet run
```

Abra o navegador e acesse:

http://localhost:5186

Ou, se estiver usando o Visual Studio, pressione F5 para executar o projeto.

### Como Usar o Sistema
#### 1. Login
 - Acesse o swagger e execute o login com o login e senha do usuário disponibilizado.

 - Após o login, um token JWT será armazenado em um cookie seguro.

#### 2. Criar Tarefa
 - Clique em Criar Tarefa.

 - Preencha o formulário com o título, status e data de vencimento.

 - Clique em Salvar para criar a tarefa.

#### 3. Editar Tarefa
 - Na lista de tarefas, clique em Editar ao lado da tarefa que deseja modificar.

 - Faça as alterações necessárias e clique em Salvar.

#### 4. Excluir Tarefa
 - Na lista de tarefas, clique em Excluir ao lado da tarefa que deseja remover.

#### 5. Filtrar Tarefas
 - Na lista de tarefas, use os filtros de status e data de vencimento para exibir apenas as tarefas que atendem aos critérios.

### Licença
 - Este projeto está licenciado sob a licença MIT. Consulte o arquivo LICENSE para mais detalhes.
