using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentReservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentIdToRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentRequest_Appointments_AppointmentId",
                table: "AppointmentRequest");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppointmentId",
                table: "AppointmentRequest",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentRequest_Appointments_AppointmentId",
                table: "AppointmentRequest",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentRequest_Appointments_AppointmentId",
                table: "AppointmentRequest");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppointmentId",
                table: "AppointmentRequest",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentRequest_Appointments_AppointmentId",
                table: "AppointmentRequest",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");
        }
    }
}
