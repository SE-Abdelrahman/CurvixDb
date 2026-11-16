using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurvixDb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                    table.CheckConstraint("CK_Patients_DOB_Past", "[DateOfBirth] <= CAST(GETUTCDATE() AS date)");
                    table.CheckConstraint("CK_Patients_Gender_Valid", "[Gender] IN ('Male', 'Female')");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    SupervisorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.CheckConstraint("CK_Users_Email_Format", "[Email] LIKE '%_@_%._%' AND [Email] NOT LIKE '% %'");
                    table.CheckConstraint("CK_Users_No_Self_Supervisor", "[SupervisorId] IS NULL OR [SupervisorId] <> [UserId]");
                    table.CheckConstraint("CK_Users_Role_Valid", "[Role] IN ('Admin', 'Radiologist', 'Radiographer')");
                    table.ForeignKey(
                        name: "FK_Users_Users_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Patient_Phones",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient_Phones", x => new { x.PatientId, x.PhoneNumber });
                    table.CheckConstraint("CK_PatientPhones_Format", "LEN([PhoneNumber]) >= 7 AND [PhoneNumber] NOT LIKE '%[^0-9+]%'");
                    table.ForeignKey(
                        name: "FK_Patient_Phones_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Examinations",
                columns: table => new
                {
                    ExamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Pending"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examinations", x => x.ExamId);
                    table.CheckConstraint("CK_Examinations_Status_Valid", "[Status] IN ('Pending', 'Analyzed', 'Completed', 'Cancelled')");
                    table.ForeignKey(
                        name: "FK_Examinations_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Examinations_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User_Phones",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Phones", x => new { x.UserId, x.PhoneNumber });
                    table.CheckConstraint("CK_UserPhones_Format", "LEN([PhoneNumber]) >= 7 AND [PhoneNumber] NOT LIKE '%[^0-9+]%'");
                    table.ForeignKey(
                        name: "FK_User_Phones_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medical_Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ViewType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsValid = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ExamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medical_Images", x => x.ImageId);
                    table.CheckConstraint("CK_MedicalImages_ViewType_Valid", "[ViewType] IN ('AP', 'PA', 'Lateral')");
                    table.ForeignKey(
                        name: "FK_Medical_Images_Examinations_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Examinations",
                        principalColumn: "ExamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AI_Predictions",
                columns: table => new
                {
                    PredictionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetectedDeformity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ConfidenceScore = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    VisualOverlayPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LowerVertebra = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApexVertebra = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MeasuredAngle = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    UpperVertebra = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AI_Predictions", x => x.PredictionId);
                    table.CheckConstraint("CK_AiPredictions_Angle_Range", "[MeasuredAngle] >= 0 AND [MeasuredAngle] <= 180");
                    table.CheckConstraint("CK_AiPredictions_Confidence_Range", "[ConfidenceScore] >= 0 AND [ConfidenceScore] <= 1");
                    table.CheckConstraint("CK_AiPredictions_Deformity_Valid", "[DetectedDeformity] IN ('Normal', 'Scoliosis', 'Kyphosis', 'Lordosis')");
                    table.ForeignKey(
                        name: "FK_AI_Predictions_Medical_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Medical_Images",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medical_Reports",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinalDeformity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FinalAngle = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ReportText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AiWasModified = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    PredictionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medical_Reports", x => x.ReportId);
                    table.CheckConstraint("CK_MedicalReports_Angle_Range", "[FinalAngle] >= 0 AND [FinalAngle] <= 180");
                    table.CheckConstraint("CK_MedicalReports_Deformity_Valid", "[FinalDeformity] IN ('Normal', 'Scoliosis', 'Kyphosis', 'Lordosis')");
                    table.ForeignKey(
                        name: "FK_Medical_Reports_AI_Predictions_PredictionId",
                        column: x => x.PredictionId,
                        principalTable: "AI_Predictions",
                        principalColumn: "PredictionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medical_Reports_Examinations_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Examinations",
                        principalColumn: "ExamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medical_Reports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AI_Predictions_ImageId",
                table: "AI_Predictions",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_CreatedByUserId",
                table: "Examinations",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_PatientId",
                table: "Examinations",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Medical_Images_ExamId",
                table: "Medical_Images",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_Medical_Reports_ExamId",
                table: "Medical_Reports",
                column: "ExamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medical_Reports_PredictionId",
                table: "Medical_Reports",
                column: "PredictionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medical_Reports_UserId",
                table: "Medical_Reports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ_Patients_MedicalNumber",
                table: "Patients",
                column: "MedicalNumber",
                unique: true,
                filter: "[MedicalNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SupervisorId",
                table: "Users",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "UQ_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medical_Reports");

            migrationBuilder.DropTable(
                name: "Patient_Phones");

            migrationBuilder.DropTable(
                name: "User_Phones");

            migrationBuilder.DropTable(
                name: "AI_Predictions");

            migrationBuilder.DropTable(
                name: "Medical_Images");

            migrationBuilder.DropTable(
                name: "Examinations");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
