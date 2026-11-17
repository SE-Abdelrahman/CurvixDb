using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurvixDb.Migrations
{
    /// <inheritdoc />
    public partial class AddStoredProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var proceduresFolder = Path.Combine(AppContext.BaseDirectory, "DatabaseScripts", "Procedures");

            string[] procedureFiles =
            {
                "sp_RegisterUserWithPhones.sql",
                "sp_RegisterPatientWithPhones.sql",
                "sp_CreateExaminationWithImage.sql",
                "sp_InvalidateImageAndCancelExam.sql",
                "sp_SaveAiPrediction.sql",
                "sp_ApproveMedicalReport.sql",
                "sp_GetUserWorklist.sql",
                "sp_GetPatientFullHistory.sql",
                "sp_GetSystemDashboardStats.sql"
            };

            foreach (var file in procedureFiles)
            {
                var sql = File.ReadAllText(Path.Combine(proceduresFolder, file));
                migrationBuilder.Sql(sql);
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP PROCEDURE IF EXISTS dbo.sp_GetSystemDashboardStats;
                DROP PROCEDURE IF EXISTS dbo.sp_GetPatientFullHistory;
                DROP PROCEDURE IF EXISTS dbo.sp_GetUserWorklist;
                DROP PROCEDURE IF EXISTS dbo.sp_ApproveMedicalReport;
                DROP PROCEDURE IF EXISTS dbo.sp_SaveAiPrediction;
                DROP PROCEDURE IF EXISTS dbo.sp_InvalidateImageAndCancelExam;
                DROP PROCEDURE IF EXISTS dbo.sp_CreateExaminationWithImage;
                DROP PROCEDURE IF EXISTS dbo.sp_RegisterPatientWithPhones;
                DROP PROCEDURE IF EXISTS dbo.sp_RegisterUserWithPhones;
            ");
        }
    }
}
