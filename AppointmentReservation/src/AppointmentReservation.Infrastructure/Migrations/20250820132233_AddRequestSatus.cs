using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentReservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRequestSatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "AppointmentRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "AppointmentRequest");
        }
    }
}
