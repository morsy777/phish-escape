using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameProfileImageColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "profileImag",
                table: "AspNetUsers",
                newName: "profileImage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "profileImage",
                table: "AspNetUsers",
                newName: "profileImag");
        }
    }
}
