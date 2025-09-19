using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentReservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAppointmentIdsFromClinic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentsIds",
                table: "Clinics");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppointmentsIds",
                table: "Clinics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
