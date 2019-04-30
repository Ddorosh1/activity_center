using Microsoft.EntityFrameworkCore.Migrations;

namespace activity_center.Migrations
{
    public partial class _3rd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DurationUnit",
                table: "Activity",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Coordinator",
                table: "Activity",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordinator",
                table: "Activity");

            migrationBuilder.AlterColumn<string>(
                name: "DurationUnit",
                table: "Activity",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
