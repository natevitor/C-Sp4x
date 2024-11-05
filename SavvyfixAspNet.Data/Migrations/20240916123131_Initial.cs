using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SavvyfixAspNet.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    id_endereco = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    cep_endereco = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false),
                    rua_endereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    num_endereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    bairro_endereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    cidade_endereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    estado_endereco = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: false),
                    pais = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.id_endereco);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    id_prod = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    preco_fixo = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    marca_prod = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    desc_proc = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    nm_prod = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    img_prod = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.id_prod);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    id_cliente = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    cpf_clie = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    nm_clie = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    senha_clie = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    id_endereco = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.id_cliente);
                    table.ForeignKey(
                        name: "FK_Clientes_Enderecos_id_endereco",
                        column: x => x.id_endereco,
                        principalTable: "Enderecos",
                        principalColumn: "id_endereco",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Atividades",
                columns: table => new
                {
                    id_atividades = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    clima_atual = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    demanda_produto = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    horario_atual = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    localizacao_atual = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    preco_variado = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    qntd_procura = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    id_cliente = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atividades", x => x.id_atividades);
                    table.ForeignKey(
                        name: "FK_Atividades_Clientes_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "Clientes",
                        principalColumn: "id_cliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    id_compra = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    qntd_prod = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    valor_compra = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    id_produto = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    id_cliente = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    id_atividades = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    nm_prod = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    especificacao_prod = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.id_compra);
                    table.ForeignKey(
                        name: "FK_Compra_Atividades_id_atividades",
                        column: x => x.id_atividades,
                        principalTable: "Atividades",
                        principalColumn: "id_atividades");
                    table.ForeignKey(
                        name: "FK_Compra_Clientes_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "Clientes",
                        principalColumn: "id_cliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Compra_Produtos_id_produto",
                        column: x => x.id_produto,
                        principalTable: "Produtos",
                        principalColumn: "id_prod",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atividades_id_cliente",
                table: "Atividades",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_id_endereco",
                table: "Clientes",
                column: "id_endereco");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_id_atividades",
                table: "Compra",
                column: "id_atividades");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_id_cliente",
                table: "Compra",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_id_produto",
                table: "Compra",
                column: "id_produto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "Atividades");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Enderecos");
        }
    }
}
