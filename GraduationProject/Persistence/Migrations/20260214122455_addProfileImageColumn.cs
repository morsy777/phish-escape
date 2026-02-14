using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addProfileImageColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "profileImag",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "profileImag",
                table: "AspNetUsers");
        }
    }
}
