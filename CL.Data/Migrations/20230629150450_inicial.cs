using System;
using Microsoft.EntityFrameworkCore.Migrations;

//foi instalado o EF CORE.RELATIONAL e o EF CORE.SQLSERVER para criar as Tabelas pelo ORM
namespace CL.Data.Migrations
{
    //CODE FIRST - Primeiro codifica e depois cria o banco de dados.
    public partial class inicial : Migration
    {
        //Mostra como a tabela será criada no banco de dados através do comando: update-database
        //O comando update-database também cria o Banco de Dados caso não exista.
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    DataNascimento = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
