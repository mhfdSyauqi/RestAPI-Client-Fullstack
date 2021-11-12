using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeAPI.Migrations
{
    public partial class addCasDel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_T_AccountRole_TB_T_Account_NIK",
                table: "TB_T_AccountRole");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_T_AccountRole_TB_T_Account_NIK",
                table: "TB_T_AccountRole",
                column: "NIK",
                principalTable: "TB_T_Account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_T_AccountRole_TB_T_Account_NIK",
                table: "TB_T_AccountRole");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_T_AccountRole_TB_T_Account_NIK",
                table: "TB_T_AccountRole",
                column: "NIK",
                principalTable: "TB_T_Account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
