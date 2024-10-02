using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Categories_CategoryModelId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Flexworkers_FlexworkerModelId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Jobs_JobModelId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_CategoryModelId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_FlexworkerModelId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_JobModelId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "CategoryModelId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "FlexworkerModelId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "JobModelId",
                table: "Skills");

            migrationBuilder.CreateTable(
                name: "FlexworkerSkill",
                columns: table => new
                {
                    FlexworkersId = table.Column<int>(type: "int", nullable: false),
                    SkillsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlexworkerSkill", x => new { x.FlexworkersId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_FlexworkerSkill_Flexworkers_FlexworkersId",
                        column: x => x.FlexworkersId,
                        principalTable: "Flexworkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlexworkerSkill_Skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSkill",
                columns: table => new
                {
                    JobsId = table.Column<int>(type: "int", nullable: false),
                    SkillsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSkill", x => new { x.JobsId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_JobSkill_Jobs_JobsId",
                        column: x => x.JobsId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobSkill_Skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CategoryId",
                table: "Skills",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FlexworkerSkill_SkillsId",
                table: "FlexworkerSkill",
                column: "SkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkill_SkillsId",
                table: "JobSkill",
                column: "SkillsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Categories_CategoryId",
                table: "Skills",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Categories_CategoryId",
                table: "Skills");

            migrationBuilder.DropTable(
                name: "FlexworkerModelSkillModel");

            migrationBuilder.DropTable(
                name: "JobModelSkillModel");

            migrationBuilder.DropIndex(
                name: "IX_Skills_CategoryId",
                table: "Skills");

            migrationBuilder.AddColumn<int>(
                name: "CategoryModelId",
                table: "Skills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FlexworkerModelId",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobModelId",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CategoryModelId",
                table: "Skills",
                column: "CategoryModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_FlexworkerModelId",
                table: "Skills",
                column: "FlexworkerModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_JobModelId",
                table: "Skills",
                column: "JobModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Categories_CategoryModelId",
                table: "Skills",
                column: "CategoryModelId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Flexworkers_FlexworkerModelId",
                table: "Skills",
                column: "FlexworkerModelId",
                principalTable: "Flexworkers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Jobs_JobModelId",
                table: "Skills",
                column: "JobModelId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }
    }
}
