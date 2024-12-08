using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTravel.BookingService.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenametblHotelDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblHoTelDetails_tblBookings_BookingId",
                table: "tblHoTelDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_tblHoTelDetails_tblRoomTypes_RoomTypeId",
                table: "tblHoTelDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblHoTelDetails",
                table: "tblHoTelDetails");

            migrationBuilder.RenameTable(
                name: "tblHoTelDetails",
                newName: "tblHotelDetails");

            migrationBuilder.RenameIndex(
                name: "IX_tblHoTelDetails_RoomTypeId",
                table: "tblHotelDetails",
                newName: "IX_tblHotelDetails_RoomTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_tblHoTelDetails_BookingId",
                table: "tblHotelDetails",
                newName: "IX_tblHotelDetails_BookingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblHotelDetails",
                table: "tblHotelDetails",
                column: "HotelDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblHotelDetails_tblBookings_BookingId",
                table: "tblHotelDetails",
                column: "BookingId",
                principalTable: "tblBookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblHotelDetails_tblRoomTypes_RoomTypeId",
                table: "tblHotelDetails",
                column: "RoomTypeId",
                principalTable: "tblRoomTypes",
                principalColumn: "RoomTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblHotelDetails_tblBookings_BookingId",
                table: "tblHotelDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_tblHotelDetails_tblRoomTypes_RoomTypeId",
                table: "tblHotelDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblHotelDetails",
                table: "tblHotelDetails");

            migrationBuilder.RenameTable(
                name: "tblHotelDetails",
                newName: "tblHoTelDetails");

            migrationBuilder.RenameIndex(
                name: "IX_tblHotelDetails_RoomTypeId",
                table: "tblHoTelDetails",
                newName: "IX_tblHoTelDetails_RoomTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_tblHotelDetails_BookingId",
                table: "tblHoTelDetails",
                newName: "IX_tblHoTelDetails_BookingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblHoTelDetails",
                table: "tblHoTelDetails",
                column: "HotelDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblHoTelDetails_tblBookings_BookingId",
                table: "tblHoTelDetails",
                column: "BookingId",
                principalTable: "tblBookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblHoTelDetails_tblRoomTypes_RoomTypeId",
                table: "tblHoTelDetails",
                column: "RoomTypeId",
                principalTable: "tblRoomTypes",
                principalColumn: "RoomTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
