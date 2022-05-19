using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerfectPolicies.Migrations
{
    public partial class addSystemmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quizes",
                columns: table => new
                {
                    QuizID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuizTopic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PassPercentage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizes", x => x.QuizID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserInfoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserInfoID);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionTopic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuizID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_Questions_Quizes_QuizID",
                        column: x => x.QuizID,
                        principalTable: "Quizes",
                        principalColumn: "QuizID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    OptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionNumber = table.Column<int>(type: "int", nullable: false),
                    CorrectAnswer = table.Column<bool>(type: "bit", nullable: false),
                    QuestionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.OptionID);
                    table.ForeignKey(
                        name: "FK_Options_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Quizes",
                columns: new[] { "QuizID", "Created", "CreatorName", "PassPercentage", "QuizTitle", "QuizTopic" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 4, 6, 10, 51, 1, 514, DateTimeKind.Local).AddTicks(1817), "Creator 1", 50, "Quiz one", "Quiz Topic one" },
                    { 2, new DateTime(2022, 4, 6, 10, 51, 1, 514, DateTimeKind.Local).AddTicks(9675), "Creator 1", 50, "Quiz Two", "Quiz Topic Two" },
                    { 3, new DateTime(2022, 4, 6, 10, 51, 1, 514, DateTimeKind.Local).AddTicks(9695), "Creator 2", 50, "Quiz three", "Quiz Topic Three" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserInfoID", "Password", "UserName" },
                values: new object[] { 1, "PerfectPolicies!22", "AdminPerfectPolicies" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionID", "ImagePath", "QuestionText", "QuestionTopic", "QuizID" },
                values: new object[,]
                {
                    { 1, null, "what is australia", "Qestion Topic one", 1 },
                    { 2, null, "Where  is australia", "Qestion Topic Two", 1 },
                    { 3, null, "where is egypt", "Qestion Topic Three", 1 },
                    { 5, null, "Where  is australia", "Qestion Topic Two", 1 },
                    { 4, null, "what is australia", "Qestion Topic one", 2 },
                    { 6, null, "where is egypt", "Qestion Topic Three", 2 }
                });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "OptionID", "CorrectAnswer", "OptionNumber", "OptionText", "QuestionID" },
                values: new object[,]
                {
                    { 1, true, 1, "option A", 1 },
                    { 2, true, 2, "option B", 1 },
                    { 3, false, 3, "option c", 1 },
                    { 4, false, 4, "option D", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Options_QuestionID",
                table: "Options",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuizID",
                table: "Questions",
                column: "QuizID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Quizes");
        }
    }
}
