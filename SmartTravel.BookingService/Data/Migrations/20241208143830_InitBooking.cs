using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTravel.BookingService.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblBookingStatuses",
                columns: table => new
                {
                    BookingStatusId = table.Column<int>(type: "int", nullable: false),
                    BookingStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBookingStatuses", x => x.BookingStatusId);
                });

            migrationBuilder.CreateTable(
                name: "tblCarTypes",
                columns: table => new
                {
                    CarTypeId = table.Column<int>(type: "int", nullable: false),
                    CarTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCarTypes", x => x.CarTypeId);
                });

            migrationBuilder.CreateTable(
                name: "tblRoomTypes",
                columns: table => new
                {
                    RoomTypeId = table.Column<int>(type: "int", nullable: false),
                    RoomTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRoomTypes", x => x.RoomTypeId);
                });

            migrationBuilder.CreateTable(
                name: "tblBookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookingStatusId = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_tblBookings_tblBookingStatuses_BookingStatusId",
                        column: x => x.BookingStatusId,
                        principalTable: "tblBookingStatuses",
                        principalColumn: "BookingStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblCarRentals",
                columns: table => new
                {
                    CarRentalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    CarTypeId = table.Column<int>(type: "int", nullable: false),
                    CarRentalStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarRentalEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PickUpLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DropOffLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCarRentals", x => x.CarRentalId);
                    table.ForeignKey(
                        name: "FK_tblCarRentals_tblBookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "tblBookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblCarRentals_tblCarTypes_CarTypeId",
                        column: x => x.CarTypeId,
                        principalTable: "tblCarTypes",
                        principalColumn: "CarTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblFlightDetails",
                columns: table => new
                {
                    FlightDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    FlightDateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FlightDateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartureLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArrivalLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlightPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFlightDetails", x => x.FlightDetailId);
                    table.ForeignKey(
                        name: "FK_tblFlightDetails_tblBookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "tblBookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblHoTelDetails",
                columns: table => new
                {
                    HotelDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    HotelDetailName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomTypeId = table.Column<int>(type: "int", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HotelPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblHoTelDetails", x => x.HotelDetailId);
                    table.ForeignKey(
                        name: "FK_tblHoTelDetails_tblBookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "tblBookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblHoTelDetails_tblRoomTypes_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "tblRoomTypes",
                        principalColumn: "RoomTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblBookings_BookingStatusId",
                table: "tblBookings",
                column: "BookingStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCarRentals_BookingId",
                table: "tblCarRentals",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCarRentals_CarTypeId",
                table: "tblCarRentals",
                column: "CarTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFlightDetails_BookingId",
                table: "tblFlightDetails",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_tblHoTelDetails_BookingId",
                table: "tblHoTelDetails",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_tblHoTelDetails_RoomTypeId",
                table: "tblHoTelDetails",
                column: "RoomTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblCarRentals");

            migrationBuilder.DropTable(
                name: "tblFlightDetails");

            migrationBuilder.DropTable(
                name: "tblHoTelDetails");

            migrationBuilder.DropTable(
                name: "tblCarTypes");

            migrationBuilder.DropTable(
                name: "tblBookings");

            migrationBuilder.DropTable(
                name: "tblRoomTypes");

            migrationBuilder.DropTable(
                name: "tblBookingStatuses");
        }
    }
}
