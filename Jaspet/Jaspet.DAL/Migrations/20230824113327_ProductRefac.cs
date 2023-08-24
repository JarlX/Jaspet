using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jaspet.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ProductRefac : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "UnitPrice",
                table: "Product",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "Product");
        }
    }
}
