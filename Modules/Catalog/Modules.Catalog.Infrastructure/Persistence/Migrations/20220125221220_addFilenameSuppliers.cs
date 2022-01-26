using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Catalog.Infrastructure.Persistence.Migrations
{
    public partial class addFilenameSuppliers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                schema: "Catalog",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                schema: "Catalog",
                table: "Suppliers");
        }
    }
}
