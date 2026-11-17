using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurvixDb.Migrations
{
    /// <inheritdoc />
    public partial class FixMedicalReportTriggerNestLevelchange1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var triggerPath = Path.Combine(AppContext.BaseDirectory, "DatabaseScripts", "Triggers", "trg_PreventCompletedExamModification.sql");
            migrationBuilder.Sql(File.ReadAllText(triggerPath));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
