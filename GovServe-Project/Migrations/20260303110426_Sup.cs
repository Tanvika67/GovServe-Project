using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovServe_Project.Migrations
{
    /// <inheritdoc />
    public partial class Sup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "ServiceReports",
                columns: table => new
                {
                    ReportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Scope = table.Column<int>(type: "int", nullable: false),
                    GeneratedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceReports", x => x.ReportID);
                });

            migrationBuilder.CreateTable(
                name: "SLADays",
                columns: table => new
                {
                    SLADayID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLADays", x => x.SLADayID);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SLA_Days = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceID);
                    table.ForeignKey(
                        name: "FK_Services_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EligibilityRules",
                columns: table => new
                {
                    RuleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceID = table.Column<int>(type: "int", nullable: false),
                    RuleDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    RuleExpression = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EligibilityRules", x => x.RuleID);
                    table.ForeignKey(
                        name: "FK_EligibilityRules_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Services",
                        principalColumn: "ServiceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequiredDocuments",
                columns: table => new
                {
                    DocumentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceID = table.Column<int>(type: "int", nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Mandatory = table.Column<bool>(type: "bit", nullable: false),
                    ServiceID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredDocuments", x => x.DocumentID);
                    table.ForeignKey(
                        name: "FK_RequiredDocuments_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Services",
                        principalColumn: "ServiceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequiredDocuments_Services_ServiceID1",
                        column: x => x.ServiceID1,
                        principalTable: "Services",
                        principalColumn: "ServiceID");
                });

            migrationBuilder.CreateTable(
                name: "WorkflowStages",
                columns: table => new
                {
                    StageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceID = table.Column<int>(type: "int", nullable: false),
                    ResponsibleRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SequenceNumber = table.Column<int>(type: "int", nullable: false),
                    SLA_Days = table.Column<int>(type: "int", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowStages", x => x.StageID);
                    table.ForeignKey(
                        name: "FK_WorkflowStages_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID");
                    table.ForeignKey(
                        name: "FK_WorkflowStages_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Services",
                        principalColumn: "ServiceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    ApplicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ServiceID = table.Column<int>(type: "int", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApplicationStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.ApplicationID);
                    table.ForeignKey(
                        name: "FK_Application_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Application_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Services",
                        principalColumn: "ServiceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Application_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Application_User_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Appeals",
                columns: table => new
                {
                    AppealID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FiledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationID = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appeals", x => x.AppealID);
                    table.ForeignKey(
                        name: "FK_Appeals_Application_ApplicationID",
                        column: x => x.ApplicationID,
                        principalTable: "Application",
                        principalColumn: "ApplicationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appeals_User_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Case",
                columns: table => new
                {
                    CaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationID = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SupervisorId = table.Column<int>(type: "int", nullable: false),
                    AssignedOfficerId = table.Column<int>(type: "int", nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsWarningSent = table.Column<bool>(type: "bit", nullable: false),
                    IsEscalated = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationID1 = table.Column<int>(type: "int", nullable: true),
                    UsersUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Case", x => x.CaseId);
                    table.ForeignKey(
                        name: "FK_Case_Application_ApplicationID",
                        column: x => x.ApplicationID,
                        principalTable: "Application",
                        principalColumn: "ApplicationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Case_Application_ApplicationID1",
                        column: x => x.ApplicationID1,
                        principalTable: "Application",
                        principalColumn: "ApplicationID");
                    table.ForeignKey(
                        name: "FK_Case_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Case_User_AssignedOfficerId",
                        column: x => x.AssignedOfficerId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Case_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Case_User_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "CitizenDocument",
                columns: table => new
                {
                    CitizenDocumentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationID = table.Column<int>(type: "int", nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VerificationStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitizenDocument", x => x.CitizenDocumentID);
                    table.ForeignKey(
                        name: "FK_CitizenDocument_Application_ApplicationID",
                        column: x => x.ApplicationID,
                        principalTable: "Application",
                        principalColumn: "ApplicationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grievance",
                columns: table => new
                {
                    GrievanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationID = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FiledDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grievance", x => x.GrievanceId);
                    table.ForeignKey(
                        name: "FK_Grievance_Application_ApplicationID",
                        column: x => x.ApplicationID,
                        principalTable: "Application",
                        principalColumn: "ApplicationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grievance_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Escalation",
                columns: table => new
                {
                    EscalationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    SupervisorId = table.Column<int>(type: "int", nullable: false),
                    PreviousOfficerId = table.Column<int>(type: "int", nullable: false),
                    NewOfficerId = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EscalationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResolvedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EscalationLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escalation", x => x.EscalationId);
                    table.ForeignKey(
                        name: "FK_Escalation_Case_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Case",
                        principalColumn: "CaseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Escalation_User_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notification_Case_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Case",
                        principalColumn: "CaseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notification_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SLARecords",
                columns: table => new
                {
                    SLARecordID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    StageID = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ServiceID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLARecords", x => x.SLARecordID);
                    table.ForeignKey(
                        name: "FK_SLARecords_Case_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Case",
                        principalColumn: "CaseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SLARecords_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Services",
                        principalColumn: "ServiceID");
                    table.ForeignKey(
                        name: "FK_SLARecords_WorkflowStages_StageID",
                        column: x => x.StageID,
                        principalTable: "WorkflowStages",
                        principalColumn: "StageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appeals_ApplicationID",
                table: "Appeals",
                column: "ApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_Appeals_UsersUserId",
                table: "Appeals",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_DepartmentID",
                table: "Application",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Application_ServiceID",
                table: "Application",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Application_UserId",
                table: "Application",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_UsersUserId",
                table: "Application",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Case_ApplicationID",
                table: "Case",
                column: "ApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_Case_ApplicationID1",
                table: "Case",
                column: "ApplicationID1",
                unique: true,
                filter: "[ApplicationID1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Case_AssignedOfficerId",
                table: "Case",
                column: "AssignedOfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_Case_DepartmentID",
                table: "Case",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Case_UserId",
                table: "Case",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Case_UsersUserId",
                table: "Case",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CitizenDocument_ApplicationID",
                table: "CitizenDocument",
                column: "ApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_EligibilityRules_ServiceID",
                table: "EligibilityRules",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Escalation_CaseId",
                table: "Escalation",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Escalation_SupervisorId",
                table: "Escalation",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Grievance_ApplicationID",
                table: "Grievance",
                column: "ApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_Grievance_UserId",
                table: "Grievance",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_CaseId",
                table: "Notification",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredDocuments_ServiceID",
                table: "RequiredDocuments",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredDocuments_ServiceID1",
                table: "RequiredDocuments",
                column: "ServiceID1");

            migrationBuilder.CreateIndex(
                name: "IX_Services_DepartmentID",
                table: "Services",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_SLARecords_CaseId",
                table: "SLARecords",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_SLARecords_ServiceID",
                table: "SLARecords",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_SLARecords_StageID",
                table: "SLARecords",
                column: "StageID");

            migrationBuilder.CreateIndex(
                name: "IX_User_DepartmentID",
                table: "User",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleID",
                table: "User",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStages_RoleID",
                table: "WorkflowStages",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStages_ServiceID",
                table: "WorkflowStages",
                column: "ServiceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appeals");

            migrationBuilder.DropTable(
                name: "CitizenDocument");

            migrationBuilder.DropTable(
                name: "EligibilityRules");

            migrationBuilder.DropTable(
                name: "Escalation");

            migrationBuilder.DropTable(
                name: "Grievance");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "RequiredDocuments");

            migrationBuilder.DropTable(
                name: "ServiceReports");

            migrationBuilder.DropTable(
                name: "SLADays");

            migrationBuilder.DropTable(
                name: "SLARecords");

            migrationBuilder.DropTable(
                name: "Case");

            migrationBuilder.DropTable(
                name: "WorkflowStages");

            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
