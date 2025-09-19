using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClinicIdsToClinicCenter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClinicsCount",
                table: "ClinicCenters");

            migrationBuilder.RenameColumn(
                name: "Subscription_SubscriptionType",
                table: "ClinicCenters",
                newName: "SubscriptionType");

            migrationBuilder.AddColumn<string>(
                name: "ClinicIds",
                table: "ClinicCenters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClinicIds",
                table: "ClinicCenters");

            migrationBuilder.RenameColumn(
                name: "SubscriptionType",
                table: "ClinicCenters",
                newName: "Subscription_SubscriptionType");

            migrationBuilder.AddColumn<int>(
                name: "ClinicsCount",
                table: "ClinicCenters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
