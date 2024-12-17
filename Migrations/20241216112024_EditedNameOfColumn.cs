using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceTask.Migrations
{
    /// <inheritdoc />
    public partial class EditedNameOfColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedAt",
                table: "Users",
                newName: "Created");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Users",
                newName: "ModifiedAt");
        }
    }
}
