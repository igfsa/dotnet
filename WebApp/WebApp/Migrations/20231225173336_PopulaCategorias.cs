using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class PopulaCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categorias (Nome, ImageUrl) VALUES ('Bebidas', 'bebidas.jpg')");
            migrationBuilder.Sql("INSERT INTO Categorias (Nome, ImageUrl) VALUES ('Salgados', 'salgados.jpg')");
            migrationBuilder.Sql("INSERT INTO Categorias (Nome, ImageUrl) VALUES ('Sobremesas', 'sobremesas.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
        }
    }
}
