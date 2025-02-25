using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCredit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cashes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cashs = table.Column<double>(type: "float", nullable: false),
                    IdCashed = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AtRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AtLastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cashes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cashes");
        }
    }
}
