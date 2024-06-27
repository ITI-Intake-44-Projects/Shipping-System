using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShippingSystem.Migrations
{
    /// <inheritdoc />
    public partial class yousefv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeePrivilege");

            migrationBuilder.DropTable(
                name: "MerchantPrivilege");

            migrationBuilder.DropTable(
                name: "PrivilegeRepresentative");

            migrationBuilder.DropColumn(
                name: "Add",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Delete",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Update",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "View",
                table: "AspNetRoles");

            migrationBuilder.CreateTable(
                name: "Privileges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Add = table.Column<bool>(type: "bit", nullable: true),
                    Update = table.Column<bool>(type: "bit", nullable: true),
                    View = table.Column<bool>(type: "bit", nullable: true),
                    Delete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privileges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupPrivilege",
                columns: table => new
                {
                    Group_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Privelege_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPrivilege", x => new { x.Group_Id, x.Privelege_Id });
                    table.ForeignKey(
                        name: "FK_GroupPrivilege_AspNetRoles_Group_Id",
                        column: x => x.Group_Id,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupPrivilege_Privileges_Privelege_Id",
                        column: x => x.Privelege_Id,
                        principalTable: "Privileges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupPrivilege_Privelege_Id",
                table: "GroupPrivilege",
                column: "Privelege_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupPrivilege");

            migrationBuilder.DropTable(
                name: "Privileges");

            migrationBuilder.AddColumn<bool>(
                name: "Add",
                table: "AspNetRoles",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Delete",
                table: "AspNetRoles",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Update",
                table: "AspNetRoles",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "View",
                table: "AspNetRoles",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeePrivilege",
                columns: table => new
                {
                    EmployeesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PrivilegesId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePrivilege", x => new { x.EmployeesId, x.PrivilegesId });
                    table.ForeignKey(
                        name: "FK_EmployeePrivilege_AspNetRoles_PrivilegesId",
                        column: x => x.PrivilegesId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePrivilege_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MerchantPrivilege",
                columns: table => new
                {
                    MerchantsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PrivilegesId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantPrivilege", x => new { x.MerchantsId, x.PrivilegesId });
                    table.ForeignKey(
                        name: "FK_MerchantPrivilege_AspNetRoles_PrivilegesId",
                        column: x => x.PrivilegesId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MerchantPrivilege_Merchants_MerchantsId",
                        column: x => x.MerchantsId,
                        principalTable: "Merchants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivilegeRepresentative",
                columns: table => new
                {
                    PrivilegesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RepresentativesId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivilegeRepresentative", x => new { x.PrivilegesId, x.RepresentativesId });
                    table.ForeignKey(
                        name: "FK_PrivilegeRepresentative_AspNetRoles_PrivilegesId",
                        column: x => x.PrivilegesId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrivilegeRepresentative_Representatives_RepresentativesId",
                        column: x => x.RepresentativesId,
                        principalTable: "Representatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePrivilege_PrivilegesId",
                table: "EmployeePrivilege",
                column: "PrivilegesId");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantPrivilege_PrivilegesId",
                table: "MerchantPrivilege",
                column: "PrivilegesId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivilegeRepresentative_RepresentativesId",
                table: "PrivilegeRepresentative",
                column: "RepresentativesId");
        }
    }
}
