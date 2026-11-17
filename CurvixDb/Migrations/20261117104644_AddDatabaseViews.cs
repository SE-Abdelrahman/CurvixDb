using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurvixDb.Migrations
{
    /// <inheritdoc />
    public partial class AddDatabaseViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var viewsFolder = Path.Combine(AppContext.BaseDirectory, "DatabaseScripts", "Views");

            string[] viewFiles =
            {
                "vw_FullMedicalReports.sql",
                "vw_PatientExaminationSummary.sql",
                "vw_AiPerformanceAudit.sql",
                "vw_UserDirectoryAndHierarchy.sql"
            };

            foreach (var file in viewFiles)
            {
                var sql = File.ReadAllText(Path.Combine(viewsFolder, file));
                migrationBuilder.Sql(sql);
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP VIEW IF EXISTS dbo.vw_UserDirectoryAndHierarchy;
                DROP VIEW IF EXISTS dbo.vw_AiPerformanceAudit;
                DROP VIEW IF EXISTS dbo.vw_PatientExaminationSummary;
                DROP VIEW IF EXISTS dbo.vw_FullMedicalReports;
            ");
        }
    }
}
