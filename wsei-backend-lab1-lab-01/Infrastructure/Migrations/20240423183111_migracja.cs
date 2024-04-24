using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendLab01.Migrations
{
    /// <inheritdoc />
    public partial class migracja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "3ca1b7dc-fbcb-452f-85d4-3507ab93db1b");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ae16657f-851a-424f-9eb7-3acaa58b07e8");
        }
    }
}
