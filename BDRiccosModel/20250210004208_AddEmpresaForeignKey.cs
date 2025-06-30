using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDRiccosModel.Migrations
{
    /// <inheritdoc />
    public partial class AddEmpresaForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
              migrationBuilder.AddForeignKey(
        name: "FK_HorariosRegulares_Empresa_IdEmpresa",
        schema: "empresa",
        table: "HorariosRegulares",
        column: "IdEmpresa",
        principalSchema: "empresa",
        principalTable: "Empresa",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade);

           


            migrationBuilder.CreateIndex(
                name: "Empresa_HorariosRegulares",
                schema: "empresa",
                table: "HorariosRegulares",
                column: "IdEmpresa");



        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropTable(
                name: "HorariosRegulares",
                schema: "empresa");

           

            migrationBuilder.DropTable(
                name: "Empresa",
                schema: "empresa");

          
        }
    }
}
