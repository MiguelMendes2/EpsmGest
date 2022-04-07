using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpsmGest.Migrations
{
    public partial class UpdateVehicles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "RequestVehicle");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "RequestSpace");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "IsDriver",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Requisition",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RequisitionId",
                table: "RequestVehicle",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RequisitionId",
                table: "RequestSpace",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RequestInterventionModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequisitionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InterventionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stage = table.Column<int>(type: "int", nullable: false),
                    EquipamentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestInterventionModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestInterventionModel_Equipment_EquipamentId",
                        column: x => x.EquipamentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestInterventionModel_Requisition_RequisitionId",
                        column: x => x.RequisitionId,
                        principalTable: "Requisition",
                        principalColumn: "RequisicaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestVehicle_RequisitionId",
                table: "RequestVehicle",
                column: "RequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestSpace_RequisitionId",
                table: "RequestSpace",
                column: "RequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestInterventionModel_EquipamentId",
                table: "RequestInterventionModel",
                column: "EquipamentId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestInterventionModel_RequisitionId",
                table: "RequestInterventionModel",
                column: "RequisitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestSpace_Requisition_RequisitionId",
                table: "RequestSpace",
                column: "RequisitionId",
                principalTable: "Requisition",
                principalColumn: "RequisicaoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestVehicle_Requisition_RequisitionId",
                table: "RequestVehicle",
                column: "RequisitionId",
                principalTable: "Requisition",
                principalColumn: "RequisicaoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestSpace_Requisition_RequisitionId",
                table: "RequestSpace");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestVehicle_Requisition_RequisitionId",
                table: "RequestVehicle");

            migrationBuilder.DropTable(
                name: "RequestInterventionModel");

            migrationBuilder.DropIndex(
                name: "IX_RequestVehicle_RequisitionId",
                table: "RequestVehicle");

            migrationBuilder.DropIndex(
                name: "IX_RequestSpace_RequisitionId",
                table: "RequestSpace");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Requisition");

            migrationBuilder.DropColumn(
                name: "RequisitionId",
                table: "RequestVehicle");

            migrationBuilder.DropColumn(
                name: "RequisitionId",
                table: "RequestSpace");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RequestVehicle",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RequestSpace",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Purchase",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDriver",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
