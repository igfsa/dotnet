using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "INSERT INTO Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                "VALUES ('Suco de Uva', 'Suco sabor Uva 250ml', 3.00, 'sucouva.jpg', 50, now(), 1)"
            );
            migrationBuilder.Sql(
                "INSERT INTO Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                "VALUES ('Pão de Batata com Frango', 'Pão de batata com recheio de frango', 5.00, 'paodebatatafrango.jpg', 25, now(), 2)"
            );
            migrationBuilder.Sql(
                "INSERT INTO Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                "VALUES ('Bolo no Pote', 'Bolo de pote', 4.50, 'bolopote.jpg', 15, now(), 3)"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Produtos");
        }
    }
}
