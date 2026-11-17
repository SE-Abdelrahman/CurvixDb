using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurvixDb.Migrations
{
    /// <inheritdoc />
    public partial class AddDatabaseIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Patients",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            var indexesFolder = Path.Combine(AppContext.BaseDirectory, "DatabaseScripts", "Indexes");

            string[] indexFiles =
            {
                "IX_Patients_Name.sql",
                "IX_Examinations_Status_ExamDate.sql",
                "IX_MedicalReports_ApprovedAt.sql",
                "IX_MedicalReports_FinalDeformity.sql"
            };

            foreach (var file in indexFiles)
            {
                var sql = File.ReadAllText(Path.Combine(indexesFolder, file));
                migrationBuilder.Sql(sql);
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.Sql(@"
                DROP INDEX IF EXISTS IX_MedicalReports_FinalDeformity ON dbo.Medical_Reports;
                DROP INDEX IF EXISTS IX_MedicalReports_ApprovedAt ON dbo.Medical_Reports;
                DROP INDEX IF EXISTS IX_Examinations_Status_ExamDate ON dbo.Examinations;
                DROP INDEX IF EXISTS IX_Patients_Name ON dbo.Patients;
            ");
        }
    }
}
