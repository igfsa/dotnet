# Migrations #

&xrArr; O recurso Migrations gera script para manutenção dos elementos do banco de dados de acordo com alterações das entidades da aplicação. 

&xrArr; Assim, a cada alteração nas classes relativas às entidades, uma nova migração deve ser criada e executada. 

## Comandos ##

* Para criar o script:
    ```console
        dotnet ef migrations add 'nome'
    ```

* Para remover o script:
    ```console
        dotnet ef migrations remove 'nome'
    ```

* Para gerar realizar as operações com base no script: 
    ```console
        dotnet ef migrations add 'nome'
    ```

---

&xrArr; Após a adição de uma migration, é possível alterar o script gerado antes de aplicar a migration no banco. É gerado um método Up() para realizar as alterações e um método Down() caso a migração seja revertida. 

&xrArr; Uma das finalidades da alteração de um script de migração pode ser popular tabelas com dados, sendo necessário inserir o conteúdo desejado no método Up(). No método Down(), deve ser inserido o código para excluir os dados. As migrações [PopulaProdutos](./20231225173301_PopulaProdutos.cs) e [PopulaCategorias](./20231225173336_PopulaCategorias.cs) realizam esse procedimento. 




