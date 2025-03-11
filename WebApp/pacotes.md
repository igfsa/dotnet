# Instalação do Entity Framework Core #

```console
    dotnet tool instal --global dotnet-ef
``` 
> A opção --global instala a ferramenta globalmente na máquina. Pode ser configurada para instalar apenas no projeto 

# Pacotes utilizados/Comando para instalação #

* Pomelo (provedor)
    ```console
        dotnet add package Pomelo.EntityFrameworkCore.MySql
    ```
    
* Entity Framework Design (ferramenta para mapear entidades para o banco)
    ```console
        dotnet add package Microsoft.EntityFrameworkCore.Designer
    ```

* Ferramentas para Data Annotations
    ```console
        dotnet add package System.ComponentModel.DataAnnotations
        dotnet add package System.ComponentModel.DataAnnotations.Schema
    ```



    