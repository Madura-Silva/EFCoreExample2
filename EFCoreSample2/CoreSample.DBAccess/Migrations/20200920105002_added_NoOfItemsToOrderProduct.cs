using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreSample.DBAccess.Migrations
{
    public partial class added_NoOfItemsToOrderProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfItems",
                table: "OrderProduct",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfItems",
                table: "OrderProduct");
        }
    }
}
