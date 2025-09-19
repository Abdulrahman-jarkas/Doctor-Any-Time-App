using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentReservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeServiceIdsColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_Availabilities_AvailabilityId",
                table: "Request");

            migrationBuilder.RenameColumn(
                name: "ServicesIds",
                table: "Rooms",
                newName: "ServiceIds");

            migrationBuilder.RenameColumn(
                name: "ServicesIds",
                table: "Doctors",
                newName: "ServiceIds");

            migrationBuilder.RenameColumn(
                name: "ServicesIds",
                table: "Appointments",
                newName: "ServiceIds");

            migrationBuilder.AlterColumn<Guid>(
                name: "AvailabilityId",
                table: "Request",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceIds",
                table: "Availabilities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Availabilities_AvailabilityId",
                table: "Request",
                column: "AvailabilityId",
                principalTable: "Availabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_Availabilities_AvailabilityId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "ServiceIds",
                table: "Availabilities");

            migrationBuilder.RenameColumn(
                name: "ServiceIds",
                table: "Rooms",
                newName: "ServicesIds");

            migrationBuilder.RenameColumn(
                name: "ServiceIds",
                table: "Doctors",
                newName: "ServicesIds");

            migrationBuilder.RenameColumn(
                name: "ServiceIds",
                table: "Appointments",
                newName: "ServicesIds");

            migrationBuilder.AlterColumn<Guid>(
                name: "AvailabilityId",
                table: "Request",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Availabilities_AvailabilityId",
                table: "Request",
                column: "AvailabilityId",
                principalTable: "Availabilities",
                principalColumn: "Id");
        }
    }
}
