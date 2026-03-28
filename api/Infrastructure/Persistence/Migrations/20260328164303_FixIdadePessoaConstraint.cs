using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialControllerServer.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixIdadePessoaConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Pessoa_Idade_Positiva",
                table: "Pessoas");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Pessoa_Idade",
                table: "Pessoas",
                sql: "\"Idade\" >= 0 AND \"Idade\" <= 150");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Pessoa_Idade",
                table: "Pessoas");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Pessoa_Idade_Positiva",
                table: "Pessoas",
                sql: "\"Idade\" > 0");
        }
    }
}
