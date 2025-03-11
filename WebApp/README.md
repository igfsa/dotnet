# Web App #

&xrArr; Este repositório demonstra uma Web API para um catálogo simples de produtos/categorias para atender uma rede de lojas/supermercados.

&xrArr; Idealiza-se: 

 * Criar um serviço RESTful que permita que aplicativos clientes gerenciem o catálogo de produtos e categorias

 * Expor endpoints para criar, ler, editar e excluir produtos e também para consultar produtos e um produto específico

   * Para produtos precisamos armazenar nome, descrição, valor unitário, caminho da imagem, estoque, data do cadastro e categoria

 * Expor endpoints para criar, ler, editar e excluir categorias e também consultar categorias, uma categoria específica e os produtos de uma categoria

   * Para categorias precisamos armazenar o nome e o caminho da imagem  

## Endpoints ##

&xrArr; Produtos

* API /v1/api/produtos

* GET /v1/api/produtos

* GET /v1/api/produtos/1

* POST /v1/api/produtos

* PUT /v1/api/produtos/1

* DELETE /v1/api/produtos/1

&xrArr; Categorias

* API /v1/api/categorias

* GET /v1/api/categorias

* GET /v1/api/categorias/1

* GET /v1/api/categorias/1/produtos

* POST /v1/api/categorias

* PUT /v1/api/categorias/1

* DELETE /v1/api/categorias/1

## Resposta JSON ## 

&xrArr; Para request GET

```json 
[
    {
        "categoriaId": 1,
        "nome": "string",
        "imageUrl": "string"
    },
    {
        "categoriaId": 2,
        "nome": "string",
        "imageUrl": "string"
    },
    {
        "categoriaId": 3,
        "nome": "string",
        "imageUrl": "string"
    },
]
```

## Implementar Segurança ##

* Permitir acesso somente a usuários autenticados

* Definir uma politica de autorização de acesso aos usuários

* Criar um serviço RESTful que permita gerenciar os usuários 

* Expor endpoints para criar, ler, editar e excluir usuários e também para consultar usuários e um usuário em específico 

* Armazenar nome, email e senha para os usuários

### Endpoints de segurança ###

* API /v1/api/usuários

* GET /v1/api/usuários

* GET /v1/api/usuários/1

* POST /v1/api/usuários

* PUT /v1/api/usuários/1

* DELETE /v1/api/usuários/1

## Definir os códigos HTTP para as respostas (tratamento de erros) ##

* 200 &rArr; OK

* 201 &rArr; Crated

* 202 &rArr; Accepted

* 204 &rArr; No Content

* 304 &rArr; Not Modified

* 400 &rArr; Bad Request

* 401 &rArr; Unauthorized

* 403 &rArr; Forbidden

* 404 &rArr; Not Found

* 409 &rArr; Conflict

* 500 &rArr; Internal Server Error  

## Persistência dos Dados ## 

&xrArr; Banco relacional: MySql

&xrArr; Será utilizado o Pomelo (Pomelo.EntityFrameworkCore.MySql)

&xrArr; Será utilizado o Entity Framework Core

&xrArr; Para criação do banco de dados e da tabela será utilizada a abordagem Code-First, partindo das entidades da aplicação para a criação das entidades no banco

&xrArr; Será utilizado o padrão repositório (desacoplar o acesso aos dados da aplicação)

## Estrutura do Repositório ## 

&xrArr; As subpastas do repositório são divididas de acordo com a finalidade de seus arquivos. 

* A pasta Context possui o arquivo de contexto, onde a conexão com o banco e o mapeamento das entidades da API com o banco é realizado

* A pasta Controllers possui os controladores, com as ações HTTP relativas as entidades

* A pasta Migrations possui as migrações para o banco de dados. Com a abordagem Code-First, uma migration pode realizar ações como criação das tabelas e inserção de valores populados

* A pasta Models possui o modelo das entidades, sendo os objetos das entidades da API. Todas as propriedades da entidade, com seus tipos e getters e setters são declarados em seus arquivos

