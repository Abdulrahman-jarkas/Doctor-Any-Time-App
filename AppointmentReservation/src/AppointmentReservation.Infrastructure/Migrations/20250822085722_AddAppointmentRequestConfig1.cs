using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentReservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentRequestConfig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentRequest_Appointments_AppointmentId",
                table: "AppointmentRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentRequest",
                table: "AppointmentRequest");

            migrationBuilder.RenameTable(
                name: "AppointmentRequest",
                newName: "AppointmentRequests");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentRequest_AppointmentId",
                table: "AppointmentRequests",
                newName: "IX_AppointmentRequests_AppointmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentRequests",
                table: "AppointmentRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentRequests_Appointments_AppointmentId",
                table: "AppointmentRequests",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentRequests_Appointments_AppointmentId",
                table: "AppointmentRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentRequests",
                table: "AppointmentRequests");

            migrationBuilder.RenameTable(
                name: "AppointmentRequests",
                newName: "AppointmentRequest");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentRequests_AppointmentId",
                table: "AppointmentRequest",
                newName: "IX_AppointmentRequest_AppointmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentRequest",
                table: "AppointmentRequest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentRequest_Appointments_AppointmentId",
                table: "AppointmentRequest",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
