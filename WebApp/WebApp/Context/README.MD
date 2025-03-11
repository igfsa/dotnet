# Context #

&xrArr; O arquivo nessa pasta realiza a conexão com o banco e o mapeamento das entidades da API. 

&xrArr; É declarada a classe AppDbCOntext, que herda de DbContext, uma classe do Entity Framework Core que realiza operações com banco de dados. 

&xrArr; Dentro dessa classe, a conexão com o banco é realizada através de um construtor que passa para a classe base (DbContext) os parâmetros de conexão. Esse construtor será utilizado em Program.cs recebendo a string de conexão com o banco de dados e realizando a conexão. 

&xrArr; Nessa classe também é declarado o mapeamento das entidades da API para o banco através de DdSet.