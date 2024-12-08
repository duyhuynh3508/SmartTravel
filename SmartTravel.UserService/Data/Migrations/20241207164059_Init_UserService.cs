using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTravel.UserService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init_UserService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblRoles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRoles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "tblPermissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CanViewBookings = table.Column<bool>(type: "bit", nullable: false),
                    CanEditBookings = table.Column<bool>(type: "bit", nullable: false),
                    CanViewPayments = table.Column<bool>(type: "bit", nullable: false),
                    CanEditPayments = table.Column<bool>(type: "bit", nullable: false),
                    CanCreateItinerary = table.Column<bool>(type: "bit", nullable: false),
                    CanUpdateItinerary = table.Column<bool>(type: "bit", nullable: false),
                    CanManageUsers = table.Column<bool>(type: "bit", nullable: false),
                    CanViewAnalytics = table.Column<bool>(type: "bit", nullable: false),
                    CanManageReviews = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPermissions", x => x.PermissionId);
                    table.ForeignKey(
                        name: "FK_tblPermissions_tblRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tblRoles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUsers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_tblUsers_tblRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tblRoles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblPermissions_RoleId",
                table: "tblPermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tblUsers_RoleId",
                table: "tblUsers",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblPermissions");

            migrationBuilder.DropTable(
                name: "tblUsers");

            migrationBuilder.DropTable(
                name: "tblRoles");
        }
    }
}
