using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentReservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRoomsFromClinic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Clinics_ClinicId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "OutboxIntegrationEvents");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_ClinicId",
                table: "Rooms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OutboxIntegrationEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxIntegrationEvents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_ClinicId",
                table: "Rooms",
                column: "ClinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Clinics_ClinicId",
                table: "Rooms",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
