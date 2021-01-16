using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreSample.DBAccess.Migrations
{
    public partial class remove_OrderProductId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderProductId",
                table: "OrderProduct");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderProductId",
                table: "OrderProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
