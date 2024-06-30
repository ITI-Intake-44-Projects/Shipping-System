using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShippingSystem.Migrations
{
    /// <inheritdoc />
    public partial class yousefv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupPrivilege_AspNetRoles_Group_Id",
                table: "GroupPrivilege");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPrivilege_Privileges_Privelege_Id",
                table: "GroupPrivilege");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupPrivilege",
                table: "GroupPrivilege");

            migrationBuilder.AlterColumn<int>(
                name: "Privelege_Id",
                table: "GroupPrivilege",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Group_Id",
                table: "GroupPrivilege",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "GroupPrivilege",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupPrivilege",
                table: "GroupPrivilege",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPrivilege_Group_Id",
                table: "GroupPrivilege",
                column: "Group_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPrivilege_AspNetRoles_Group_Id",
                table: "GroupPrivilege",
                column: "Group_Id",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPrivilege_Privileges_Privelege_Id",
                table: "GroupPrivilege",
                column: "Privelege_Id",
                principalTable: "Privileges",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupPrivilege_AspNetRoles_Group_Id",
                table: "GroupPrivilege");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPrivilege_Privileges_Privelege_Id",
                table: "GroupPrivilege");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupPrivilege",
                table: "GroupPrivilege");

            migrationBuilder.DropIndex(
                name: "IX_GroupPrivilege_Group_Id",
                table: "GroupPrivilege");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GroupPrivilege");

            migrationBuilder.AlterColumn<int>(
                name: "Privelege_Id",
                table: "GroupPrivilege",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Group_Id",
                table: "GroupPrivilege",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupPrivilege",
                table: "GroupPrivilege",
                columns: new[] { "Group_Id", "Privelege_Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPrivilege_AspNetRoles_Group_Id",
                table: "GroupPrivilege",
                column: "Group_Id",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPrivilege_Privileges_Privelege_Id",
                table: "GroupPrivilege",
                column: "Privelege_Id",
                principalTable: "Privileges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
