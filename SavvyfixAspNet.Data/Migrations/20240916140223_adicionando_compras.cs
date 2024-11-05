using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SavvyfixAspNet.Data.Migrations
{
    /// <inheritdoc />
    public partial class adicionando_compras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Atividades_id_atividades",
                table: "Compra");

            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Clientes_id_cliente",
                table: "Compra");

            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Produtos_id_produto",
                table: "Compra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Compra",
                table: "Compra");

            migrationBuilder.RenameTable(
                name: "Compra",
                newName: "Compras");

            migrationBuilder.RenameIndex(
                name: "IX_Compra_id_produto",
                table: "Compras",
                newName: "IX_Compras_id_produto");

            migrationBuilder.RenameIndex(
                name: "IX_Compra_id_cliente",
                table: "Compras",
                newName: "IX_Compras_id_cliente");

            migrationBuilder.RenameIndex(
                name: "IX_Compra_id_atividades",
                table: "Compras",
                newName: "IX_Compras_id_atividades");

            migrationBuilder.AlterColumn<decimal>(
                name: "preco_fixo",
                table: "Produtos",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "preco_variado",
                table: "Atividades",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "valor_compra",
                table: "Compras",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Compras",
                table: "Compras",
                column: "id_compra");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Atividades_id_atividades",
                table: "Compras",
                column: "id_atividades",
                principalTable: "Atividades",
                principalColumn: "id_atividades");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Clientes_id_cliente",
                table: "Compras",
                column: "id_cliente",
                principalTable: "Clientes",
                principalColumn: "id_cliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Produtos_id_produto",
                table: "Compras",
                column: "id_produto",
                principalTable: "Produtos",
                principalColumn: "id_prod",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Atividades_id_atividades",
                table: "Compras");

            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Clientes_id_cliente",
                table: "Compras");

            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Produtos_id_produto",
                table: "Compras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Compras",
                table: "Compras");

            migrationBuilder.RenameTable(
                name: "Compras",
                newName: "Compra");

            migrationBuilder.RenameIndex(
                name: "IX_Compras_id_produto",
                table: "Compra",
                newName: "IX_Compra_id_produto");

            migrationBuilder.RenameIndex(
                name: "IX_Compras_id_cliente",
                table: "Compra",
                newName: "IX_Compra_id_cliente");

            migrationBuilder.RenameIndex(
                name: "IX_Compras_id_atividades",
                table: "Compra",
                newName: "IX_Compra_id_atividades");

            migrationBuilder.AlterColumn<decimal>(
                name: "preco_fixo",
                table: "Produtos",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "preco_variado",
                table: "Atividades",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "valor_compra",
                table: "Compra",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Compra",
                table: "Compra",
                column: "id_compra");

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Atividades_id_atividades",
                table: "Compra",
                column: "id_atividades",
                principalTable: "Atividades",
                principalColumn: "id_atividades");

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Clientes_id_cliente",
                table: "Compra",
                column: "id_cliente",
                principalTable: "Clientes",
                principalColumn: "id_cliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Produtos_id_produto",
                table: "Compra",
                column: "id_produto",
                principalTable: "Produtos",
                principalColumn: "id_prod",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
