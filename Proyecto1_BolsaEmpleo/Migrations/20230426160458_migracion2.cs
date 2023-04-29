using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto1_BolsaEmpleo.Migrations
{
    /// <inheritdoc />
    public partial class migracion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidato_Habilidad_HabilidadId",
                table: "Candidato");

            migrationBuilder.DropIndex(
                name: "IX_Candidato_HabilidadId",
                table: "Candidato");

            migrationBuilder.DropColumn(
                name: "HabilidadId",
                table: "Candidato");

            migrationBuilder.CreateTable(
                name: "CandidatoHabilidad",
                columns: table => new
                {
                    candidatosId = table.Column<int>(type: "int", nullable: false),
                    habilidadesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidatoHabilidad", x => new { x.candidatosId, x.habilidadesId });
                    table.ForeignKey(
                        name: "FK_CandidatoHabilidad_Candidato_candidatosId",
                        column: x => x.candidatosId,
                        principalTable: "Candidato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidatoHabilidad_Habilidad_habilidadesId",
                        column: x => x.habilidadesId,
                        principalTable: "Habilidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmpresaHabilidad",
                columns: table => new
                {
                    empresasId = table.Column<int>(type: "int", nullable: false),
                    habilidadesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaHabilidad", x => new { x.empresasId, x.habilidadesId });
                    table.ForeignKey(
                        name: "FK_EmpresaHabilidad_Empresa_empresasId",
                        column: x => x.empresasId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpresaHabilidad_Habilidad_habilidadesId",
                        column: x => x.habilidadesId,
                        principalTable: "Habilidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidatoHabilidad_habilidadesId",
                table: "CandidatoHabilidad",
                column: "habilidadesId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaHabilidad_habilidadesId",
                table: "EmpresaHabilidad",
                column: "habilidadesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidatoHabilidad");

            migrationBuilder.DropTable(
                name: "EmpresaHabilidad");

            migrationBuilder.AddColumn<int>(
                name: "HabilidadId",
                table: "Candidato",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidato_HabilidadId",
                table: "Candidato",
                column: "HabilidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidato_Habilidad_HabilidadId",
                table: "Candidato",
                column: "HabilidadId",
                principalTable: "Habilidad",
                principalColumn: "Id");
        }
    }
}
