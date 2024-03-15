# MusicEvents

## Sobre
Este é um sistema para divulgação de eventos onde um usuário ADMINISTRADOR cria os eventos e 
outro usuário CLIENTE faz sua inscrição nos eventos criados.

Para a interação com o sistema, ambos os usuários precisam se autenticar através de e-mail e senha.
O usuário CLIENTE pode criar sua conta utilizando a página de cadastro e para um melhor aproveitamento, 
um usuário ADMINISTRADOR já foi previamente cadastrado com as seguintes credenciais. 

E-mail: admin@musicevents.com  <br>
Senha: admin123

# Documentação

* [Tecnologias utilizadas](#tecnologias-utilizadas)

## Protótipos
### Tela de login
<a href="#">![Tela de login!](MusicEvent.Web/ClientApp/src/assets/img/tela-login.png "Tela de login")</a>

### Tela de cadastro de usuário
<a href="#">![Tela de cadastro de usuário!](MusicEvent.Web/ClientApp/src/assets/img/tela-criar-conta.png "Tela de cadastro")</a>

### Página inicial do administrador
<a href="#">![Página Inicial - Administrador!](MusicEvent.Web/ClientApp/src/assets/img/admin-home.png "Página Inicial - Administrador")</a>

### Página inicial do cliente
<a href="#">![Página Inicial - Cliente!](MusicEvent.Web/ClientApp/src/assets/img/home-cliente.png "Página Inicial - Cliente")</a>

## Como executar o projeto
* Para clonar o projeto com o Visual Studio 2022, clique em "Clone a repository", em seguida "GitHub" e entre com as credenciais de sua conta e url do projeto.
* Ao clonar o projeto, deve ser executado o arquivo no diretório "MusicEvent\MusicEvent.sln".
* Para executar o migration da criação do DB, no menu "Tools" > "NuGet Package Manager" > "Package Manager Console", selecione o "Default project: 4. Infrastructure\MusicEvent.Infra.Data" e execute o comando "update-database -context MusicEventContext"
* Para restaurar a base criada no SQL Server Management Studio, clique com o direito em "Databases" > "Restore Database..." e selecione o arquivo na pasta "MusicEvent\SQL\DB_MusicEvent.bak"
* Para executar o projeto, clique com o botão direito no projeto web "MusicEvent.Web", selecione "Set as Startup Project" e no botão "executar" na parte superior central, selecione "IIS Express" e clique em executar para carregar a página do Swagger.
* Para executar o Front-End(Angular) utilizando o Visual Studio Code, clique em "Open Folder...", selecione a pasta "MusicEvent\MusicEvent.Web\ClientApp", em seguida no terminal do Visual Studio Code, execute o comando "npm install", e ao concluir execute o comando "ng serve -o" para executar a aplicação e abrir a página de login.

### Tecnologias utilizadas
* .NET 7
* EntityFrameworkCore
* SQL Server
* Angular 15
