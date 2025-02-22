Os passos são similares para criação de uma minimal Api comum 

$ dotnet new web -o WebApi_minimalApi


Adicionando pacotes

-> Swagger
$ dotnet add package Swashbuckle.AspNetCore

-> Dapper
dotnet add package Dapper
dotnet add package Dapper.Contrib

-> MySqlConnector
dotnet add package MySqlConnector


No projeto definiram-se 3 diretórios: 

--> Data
 -> definir o record (recurso do c# 9, é um tipo de dados imutável e é somente leitura. Será utilizado para definir os modelos)
 -> definir o delegate

-> Endpoints


-> Extensions







