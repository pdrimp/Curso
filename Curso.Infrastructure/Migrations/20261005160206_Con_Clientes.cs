using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Curso.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Con_Clientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_TiposClientes_TipoClienteId",
                        column: x => x.TipoClienteId,
                        principalTable: "TiposClientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClienteInteres",
                columns: table => new
                {
                    ClientesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InteresesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteInteres", x => new { x.ClientesId, x.InteresesId });
                    table.ForeignKey(
                        name: "FK_ClienteInteres_Clientes_ClientesId",
                        column: x => x.ClientesId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClienteInteres_Intereses_InteresesId",
                        column: x => x.InteresesId,
                        principalTable: "Intereses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClienteInteres_InteresesId",
                table: "ClienteInteres",
                column: "InteresesId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Correo",
                table: "Clientes",
                column: "Correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_TipoClienteId",
                table: "Clientes",
                column: "TipoClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteInteres");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
