using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HalaqahModel.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Masjid",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masjid", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Record",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Record", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Semester",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semester", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    MasjidID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Person_Masjid",
                        column: x => x.MasjidID,
                        principalTable: "Masjid",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Segment",
                columns: table => new
                {
                    RecordID = table.Column<int>(type: "integer", nullable: false),
                    SegmentID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SurahFrom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AyahFrom = table.Column<int>(type: "integer", nullable: false),
                    SurahTo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    AyahTo = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segment", x => new { x.SegmentID, x.RecordID });
                    table.ForeignKey(
                        name: "FK_Segment_Record",
                        column: x => x.RecordID,
                        principalTable: "Record",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    Grade = table.Column<int>(type: "integer", nullable: false),
                    ParentPhone = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Student_Person_ID",
                        column: x => x.ID,
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Nationality = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    IsTeacher = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_Person_ID",
                        column: x => x.ID,
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HalaqahRecord",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    HifzRecordID = table.Column<int>(type: "integer", nullable: true),
                    RevisionRecordID = table.Column<int>(type: "integer", nullable: true),
                    HifzNumPages = table.Column<int>(type: "integer", nullable: false),
                    HifzTargetNumPages = table.Column<int>(type: "integer", nullable: false),
                    RevisionNumPages = table.Column<int>(type: "integer", nullable: false),
                    RevisionTargetNumPages = table.Column<int>(type: "integer", nullable: false),
                    TathbeetNumPages = table.Column<int>(type: "integer", nullable: false),
                    TathbeetTargetNumPages = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tmp_ms_x__F4142FA1F4F98435", x => new { x.Date, x.StudentID });
                    table.ForeignKey(
                        name: "FK_HalaqahRecord_HifzRecord",
                        column: x => x.HifzRecordID,
                        principalTable: "Record",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_HalaqahRecord_RevisionRecord",
                        column: x => x.RevisionRecordID,
                        principalTable: "Record",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_HalaqahRecord_Student",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SemesterRecord",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "integer", nullable: false),
                    SemesterID = table.Column<int>(type: "integer", nullable: false),
                    HifzExistingRecord = table.Column<int>(type: "integer", nullable: true),
                    HifzTargetRecord = table.Column<int>(type: "integer", nullable: false),
                    HifzTargetNumPages = table.Column<int>(type: "integer", nullable: false),
                    RevisionExistingRecord = table.Column<int>(type: "integer", nullable: true),
                    RevisionTargetRecord = table.Column<int>(type: "integer", nullable: false),
                    RevisionTargetNumPages = table.Column<int>(type: "integer", nullable: false),
                    ExamGrade = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterRecord", x => new { x.StudentID, x.SemesterID });
                    table.ForeignKey(
                        name: "FK_SemesterRecord_HifzExistingRecord",
                        column: x => x.HifzExistingRecord,
                        principalTable: "Record",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SemesterRecord_HifzTargetRecord",
                        column: x => x.HifzTargetRecord,
                        principalTable: "Record",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SemesterRecord_RevisionExistingRecord",
                        column: x => x.RevisionExistingRecord,
                        principalTable: "Record",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SemesterRecord_RevisionTargetRecord",
                        column: x => x.RevisionTargetRecord,
                        principalTable: "Record",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SemesterRecord_Semester",
                        column: x => x.SemesterID,
                        principalTable: "Semester",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SemesterRecord_Student",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "StudentAttendance",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "integer", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    HasCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    HasDress = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAttendance", x => new { x.StudentID, x.Timestamp });
                    table.ForeignKey(
                        name: "FK_StudentAttendance_Student",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Halaqah",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    AdminID = table.Column<int>(type: "integer", nullable: false),
                    TeacherID = table.Column<int>(type: "integer", nullable: false),
                    SemesterID = table.Column<int>(type: "integer", nullable: false),
                    Grade = table.Column<int>(type: "integer", nullable: false),
                    WeekDays = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Halaqah", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Halaqah_Semester",
                        column: x => x.SemesterID,
                        principalTable: "Semester",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Halaqah_UserAdmin",
                        column: x => x.AdminID,
                        principalTable: "User",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Halaqah_UserTeacher",
                        column: x => x.TeacherID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "UserAttendance",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => new { x.UserID, x.Timestamp });
                    table.ForeignKey(
                        name: "FK_Attendance_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "HalaqahStudent",
                columns: table => new
                {
                    HalaqahID = table.Column<int>(type: "integer", nullable: false),
                    StudentID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HalaqahStudent", x => new { x.HalaqahID, x.StudentID });
                    table.ForeignKey(
                        name: "FK_HalaqahStudent_Halaqah",
                        column: x => x.HalaqahID,
                        principalTable: "Halaqah",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_HalaqahStudent_Student",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Halaqah_AdminID",
                table: "Halaqah",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_Halaqah_SemesterID",
                table: "Halaqah",
                column: "SemesterID");

            migrationBuilder.CreateIndex(
                name: "IX_Halaqah_TeacherID",
                table: "Halaqah",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_HalaqahRecord_HifzRecordID",
                table: "HalaqahRecord",
                column: "HifzRecordID");

            migrationBuilder.CreateIndex(
                name: "IX_HalaqahRecord_RevisionRecordID",
                table: "HalaqahRecord",
                column: "RevisionRecordID");

            migrationBuilder.CreateIndex(
                name: "IX_HalaqahRecord_StudentID",
                table: "HalaqahRecord",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_HalaqahStudent_StudentID",
                table: "HalaqahStudent",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Person_MasjidID",
                table: "Person",
                column: "MasjidID");

            migrationBuilder.CreateIndex(
                name: "IX_Segment_RecordID",
                table: "Segment",
                column: "RecordID");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterRecord_HifzExistingRecord",
                table: "SemesterRecord",
                column: "HifzExistingRecord");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterRecord_HifzTargetRecord",
                table: "SemesterRecord",
                column: "HifzTargetRecord");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterRecord_RevisionExistingRecord",
                table: "SemesterRecord",
                column: "RevisionExistingRecord");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterRecord_RevisionTargetRecord",
                table: "SemesterRecord",
                column: "RevisionTargetRecord");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterRecord_SemesterID",
                table: "SemesterRecord",
                column: "SemesterID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HalaqahRecord");

            migrationBuilder.DropTable(
                name: "HalaqahStudent");

            migrationBuilder.DropTable(
                name: "Segment");

            migrationBuilder.DropTable(
                name: "SemesterRecord");

            migrationBuilder.DropTable(
                name: "StudentAttendance");

            migrationBuilder.DropTable(
                name: "UserAttendance");

            migrationBuilder.DropTable(
                name: "Halaqah");

            migrationBuilder.DropTable(
                name: "Record");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Semester");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Masjid");
        }
    }
}
