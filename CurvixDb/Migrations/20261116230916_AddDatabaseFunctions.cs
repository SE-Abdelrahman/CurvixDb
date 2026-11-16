using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurvixDb.Migrations
{
    /// <inheritdoc />
    public partial class AddDatabaseFunctions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var functionsFolder = Path.Combine(AppContext.BaseDirectory, "DatabaseScripts", "Functions");

            string[] functionFiles =
            {
                "fn_CalculatePatientAge.sql",
                "fn_ClassifyDeformitySeverity.sql",
                "fn_GetSpinalRegion.sql",
                "fn_GetAngleDifference.sql",
                "fn_IsExamReadyForReport.sql",
                "fn_GetPatientAngleProgression.sql",
                "fn_GetSubordinateUsers.sql"
            };

            foreach (var file in functionFiles)
            {
                var sql = File.ReadAllText(Path.Combine(functionsFolder, file));
                migrationBuilder.Sql(sql);
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP FUNCTION IF EXISTS dbo.fn_GetSubordinateUsers;
                DROP FUNCTION IF EXISTS dbo.fn_GetPatientAngleProgression;
                DROP FUNCTION IF EXISTS dbo.fn_IsExamReadyForReport;
                DROP FUNCTION IF EXISTS dbo.fn_GetAngleDifference;
                DROP FUNCTION IF EXISTS dbo.fn_GetSpinalRegion;
                DROP FUNCTION IF EXISTS dbo.fn_ClassifyDeformitySeverity;
                DROP FUNCTION IF EXISTS dbo.fn_CalculatePatientAge;
            ");
        }
    }
}
