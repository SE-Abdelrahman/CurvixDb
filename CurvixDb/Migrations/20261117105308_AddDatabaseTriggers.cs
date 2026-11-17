using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurvixDb.Migrations
{
    /// <inheritdoc />
    public partial class AddDatabaseTriggers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var triggersFolder = Path.Combine(AppContext.BaseDirectory, "DatabaseScripts", "Triggers");

            string[] triggerFiles =
            {
                "trg_AfterInsertAiPrediction.sql",
                "trg_AfterInsertMedicalReport.sql",
                "trg_PreventCompletedExamModification.sql"
            };

            foreach (var file in triggerFiles)
            {
                var sql = File.ReadAllText(Path.Combine(triggersFolder, file));
                migrationBuilder.Sql(sql);
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP TRIGGER IF EXISTS dbo.trg_PreventCompletedExamModification;
                DROP TRIGGER IF EXISTS dbo.trg_AfterInsertMedicalReport;
                DROP TRIGGER IF EXISTS dbo.trg_AfterInsertAiPrediction;
            ");
        }
    }
}
