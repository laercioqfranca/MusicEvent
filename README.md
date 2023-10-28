# MusicEvents

## Sobre
Este é um sistema para divulgação de eventos onde um usuário ADMINISTRADOR cria os eventos e 
outro usuário CLIENTE faz sua inscrição.

Para a interação com o sistema, ambos os usuários precisam se autenticar através de e-mail e senha.
O usuário CLIENTE pode criar sua conta utilizando a página de cadastro e para um melhor aproveitamento, 
um usuário ADMINISTRADOR já foi previamente cadastrado com as seguintes credenciais. 

E-mail:  <br>
Senha:

# Documentação

## Requisitos

1. Construir uma API com as seguintes funcionalidades: <br>
- Autenticação: login, alteração de senha <br>
- Usuario: Cadastro, edição, listagem e exclusão <br>
- Eventos: Cadastro, edição, listagem e exclusão <br>
- Inscrição: Cadastro, exclusão e busca por ID <br>

2. Registrar os logs no banco de dados
3. Tela de login
4. Tela de cadastro de usuários
5. Página inical do Administrador
6. Página inicial do cliente
7. O sistema precisa ter dois tipos de usuários:
  * Administrador (Tem a permissão de cadastrar, listar, editar e excluir eventos)
  * Cliente (Tem a permissão de se inscrever e cancelar a inscrição de um ou mais eventos)

## Critérios de aceite
Tela login
* Permitir o acesso de um usuário existente atrvés da autenticação utilizando e-mail e senha

Tela de cadastro de usuário
* Para realizar o cadastro, o usuário precisa informar: 
  * Nome
  * Um email válido
  * Idade
  * Senha com no mínimo 8 caracteres
* Todos os campos são obrigatórios e o formulário não deve liberar o botão de enviar enquanto todos os campos não forem preenchidos corretamente

## Como executar o projeto
### Tecnologias utilizadas
* .NET 7
* EntityFrameworkCore
* SQL Server
* Angular 15
