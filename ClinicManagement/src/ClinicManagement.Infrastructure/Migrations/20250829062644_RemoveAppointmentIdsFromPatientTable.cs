using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAppointmentIdsFromPatientTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServicesIds",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ServicesIds",
                table: "Doctors");

            migrationBuilder.AddColumn<string>(
                name: "ServiceIds",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServiceIds",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceIds",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ServiceIds",
                table: "Doctors");

            migrationBuilder.AddColumn<string>(
                name: "ServicesIds",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServicesIds",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
