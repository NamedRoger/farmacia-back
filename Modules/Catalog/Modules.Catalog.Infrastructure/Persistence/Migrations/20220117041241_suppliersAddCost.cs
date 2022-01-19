using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Catalog.Infrastructure.Persistence.Migrations
{
    public partial class suppliersAddCost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Conversion",
                schema: "Catalog",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                schema: "Catalog",
                table: "Suppliers",
                type: "decimal(23,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TypeConversion",
                schema: "Catalog",
                table: "Suppliers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Conversion",
                schema: "Catalog",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Cost",
                schema: "Catalog",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "TypeConversion",
                schema: "Catalog",
                table: "Suppliers");
        }
    }
}
