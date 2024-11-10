using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Amin.Migrations
{
    /// <inheritdoc />
    public partial class Patient_MNG : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Content = table.Column<string>(type: "ntext", nullable: true),
                    Send_Day = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__14707F342513C431", x => x.NotificateID);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Post_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Content = table.Column<string>(type: "ntext", nullable: true),
                    Author_Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Posted_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Post_Image_ID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Post_Image_Data = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Posts__5875F74D65BE3C04", x => x.Post_ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Full_Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Year_Of_Birth = table.Column<DateOnly>(type: "date", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    BMI = table.Column<decimal>(type: "decimal(4,1)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Smoking_status = table.Column<bool>(type: "bit", nullable: true),
                    Alcoholic_status = table.Column<bool>(type: "bit", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__206D91904F968F0E", x => x.User_ID);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Comment_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Post_ID = table.Column<int>(type: "int", nullable: false),
                    Comment_Content = table.Column<string>(type: "ntext", nullable: true),
                    Comment_Author_ID = table.Column<int>(type: "int", nullable: true),
                    Comment_Date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Comments__99FC143B4CE8B184", x => x.Comment_ID);
                    table.ForeignKey(
                        name: "FK_Comments_AuthorID",
                        column: x => x.Comment_Author_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID");
                    table.ForeignKey(
                        name: "FK_Comments_PostID",
                        column: x => x.Post_ID,
                        principalTable: "Posts",
                        principalColumn: "Post_ID");
                });

            migrationBuilder.CreateTable(
                name: "Patient_Informations",
                columns: table => new
                {
                    Record_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    Physical_Activity_Duration = table.Column<int>(type: "int", nullable: true),
                    Caffeine_intake = table.Column<decimal>(type: "decimal(4,1)", nullable: true),
                    Sleep_Time = table.Column<TimeOnly>(type: "time", nullable: true),
                    Wake_Time = table.Column<TimeOnly>(type: "time", nullable: true),
                    User_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Patient___603A0C60E46CD560", x => x.Record_ID);
                    table.ForeignKey(
                        name: "FK_PatientInfo_UserID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID");
                });

            migrationBuilder.CreateTable(
                name: "Truy_Cap",
                columns: table => new
                {
                    User_ID = table.Column<int>(type: "int", nullable: false),
                    Post_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Truy_Cap__E5EACEE4E8E8D7B5", x => new { x.User_ID, x.Post_ID });
                    table.ForeignKey(
                        name: "FK_TruyCap_PostID",
                        column: x => x.Post_ID,
                        principalTable: "Posts",
                        principalColumn: "Post_ID");
                    table.ForeignKey(
                        name: "FK_TruyCap_UserID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Comment_Author_ID",
                table: "Comments",
                column: "Comment_Author_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Post_ID",
                table: "Comments",
                column: "Post_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Informations_User_ID",
                table: "Patient_Informations",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Truy_Cap_Post_ID",
                table: "Truy_Cap",
                column: "Post_ID");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__536C85E49A7B7A13",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Patient_Informations");

            migrationBuilder.DropTable(
                name: "Truy_Cap");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
