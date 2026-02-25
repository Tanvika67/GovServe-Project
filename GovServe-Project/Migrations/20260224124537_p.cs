using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovServe_Project.Migrations
{
    /// <inheritdoc />
<<<<<<<< HEAD:GovServe-Project/Migrations/20260225141901_supervisor.cs
    public partial class supervisor : Migration
========
    public partial class p : Migration
>>>>>>>> 4f27f3ebb5d0a396421aee5dc4c6651122ec8e48:GovServe-Project/Migrations/20260224124537_p.cs
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
<<<<<<<< HEAD:GovServe-Project/Migrations/20260225141901_supervisor.cs
                name: "Grievance",
                columns: table => new
                {
                    GrievanceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitizenID = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FiledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ForwardedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grievance", x => x.GrievanceID);
                });

            migrationBuilder.CreateTable(
========
>>>>>>>> 4f27f3ebb5d0a396421aee5dc4c6651122ec8e48:GovServe-Project/Migrations/20260224124537_p.cs
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
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
<<<<<<<< HEAD:GovServe-Project/Migrations/20260225141901_supervisor.cs
                });

            migrationBuilder.CreateTable(
                name: "Appeal",
                columns: table => new
                {
                    AppealID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrievanceID = table.Column<int>(type: "int", nullable: false),
                    CitizenID = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AppealStatus = table.Column<int>(type: "int", nullable: false),
                    AppealDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appeal", x => x.AppealID);
                    table.ForeignKey(
                        name: "FK_Appeal_Grievance_GrievanceID",
                        column: x => x.GrievanceID,
                        principalTable: "Grievance",
                        principalColumn: "GrievanceID",
                        onDelete: ReferentialAction.Cascade);
========
>>>>>>>> 4f27f3ebb5d0a396421aee5dc4c6651122ec8e48:GovServe-Project/Migrations/20260224124537_p.cs
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
                    ResponsibleRole = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SequenceNumber = table.Column<int>(type: "int", nullable: false),
                    SLA_Days = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowStages", x => x.StageID);
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
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
<<<<<<<< HEAD:GovServe-Project/Migrations/20260225141901_supervisor.cs
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubmittedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredDocumentDocumentID = table.Column<int>(type: "int", nullable: true),
                    ServiceID1 = table.Column<int>(type: "int", nullable: true)
========
                    SubmittedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApplicationStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RequiredDocumentDocumentID = table.Column<int>(type: "int", nullable: true)
>>>>>>>> 4f27f3ebb5d0a396421aee5dc4c6651122ec8e48:GovServe-Project/Migrations/20260224124537_p.cs
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
                        name: "FK_Application_RequiredDocuments_RequiredDocumentDocumentID",
                        column: x => x.RequiredDocumentDocumentID,
                        principalTable: "RequiredDocuments",
                        principalColumn: "DocumentID");
                    table.ForeignKey(
                        name: "FK_Application_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Services",
                        principalColumn: "ServiceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Application_Services_ServiceID1",
                        column: x => x.ServiceID1,
                        principalTable: "Services",
                        principalColumn: "ServiceID");
                    table.ForeignKey(
                        name: "FK_Application_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Case",
                columns: table => new
                {
                    CaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
<<<<<<<< HEAD:GovServe-Project/Migrations/20260225141901_supervisor.cs
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    SupervisorId = table.Column<int>(type: "int", nullable: false),
                    AssignedOfficerId = table.Column<int>(type: "int", nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsWarningSent = table.Column<bool>(type: "bit", nullable: false),
                    IsEscalated = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sladays = table.Column<int>(type: "int", nullable: false)
========
                    CaseID = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    StageID = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ServiceID = table.Column<int>(type: "int", nullable: true)
>>>>>>>> 4f27f3ebb5d0a396421aee5dc4c6651122ec8e48:GovServe-Project/Migrations/20260224124537_p.cs
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Case", x => x.CaseId);
                    table.ForeignKey(
                        name: "FK_Case_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "ApplicationID",
                        onDelete: ReferentialAction.Restrict);
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
                });

            migrationBuilder.CreateTable(
                name: "Case",
                columns: table => new
                {
                    CaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    SupervisorId = table.Column<int>(type: "int", nullable: false),
                    AssignedOfficerId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsEscalated = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SlaHours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Case", x => x.CaseId);
                    table.ForeignKey(
                        name: "FK_Case_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "ApplicationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Case_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CitizenDocument",
                columns: table => new
                {
                    CitizenDocumentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationID = table.Column<int>(type: "int", nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "Escalation",
                columns: table => new
                {
                    EscalationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    EscalatedByUserId = table.Column<int>(type: "int", nullable: false),
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
<<<<<<<< HEAD:GovServe-Project/Migrations/20260225141901_supervisor.cs
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Escalation_User_EscalatedByUserId",
                        column: x => x.EscalatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
========
                        onDelete: ReferentialAction.Cascade);
>>>>>>>> 4f27f3ebb5d0a396421aee5dc4c6651122ec8e48:GovServe-Project/Migrations/20260224124537_p.cs
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
<<<<<<<< HEAD:GovServe-Project/Migrations/20260225141901_supervisor.cs
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
========
>>>>>>>> 4f27f3ebb5d0a396421aee5dc4c6651122ec8e48:GovServe-Project/Migrations/20260224124537_p.cs
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
<<<<<<<< HEAD:GovServe-Project/Migrations/20260225141901_supervisor.cs
                        onDelete: ReferentialAction.Restrict);
========
                        onDelete: ReferentialAction.Cascade);
>>>>>>>> 4f27f3ebb5d0a396421aee5dc4c6651122ec8e48:GovServe-Project/Migrations/20260224124537_p.cs
                    table.ForeignKey(
                        name: "FK_Notification_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
<<<<<<<< HEAD:GovServe-Project/Migrations/20260225141901_supervisor.cs
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appeal_GrievanceID",
                table: "Appeal",
                column: "GrievanceID");

            migrationBuilder.CreateIndex(
========
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
>>>>>>>> 4f27f3ebb5d0a396421aee5dc4c6651122ec8e48:GovServe-Project/Migrations/20260224124537_p.cs
                name: "IX_Application_DepartmentID",
                table: "Application",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Application_RequiredDocumentDocumentID",
                table: "Application",
                column: "RequiredDocumentDocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_Application_ServiceID",
                table: "Application",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Application_ServiceID1",
                table: "Application",
                column: "ServiceID1");

            migrationBuilder.CreateIndex(
                name: "IX_Application_UserId",
                table: "Application",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Case_ApplicationId",
                table: "Case",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
<<<<<<<< HEAD:GovServe-Project/Migrations/20260225141901_supervisor.cs
                name: "IX_Case_AssignedOfficerId",
                table: "Case",
                column: "AssignedOfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_Case_DepartmentID",
                table: "Case",
                column: "DepartmentID");
========
                name: "IX_Case_DepartmentId",
                table: "Case",
                column: "DepartmentId");
>>>>>>>> 4f27f3ebb5d0a396421aee5dc4c6651122ec8e48:GovServe-Project/Migrations/20260224124537_p.cs

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
<<<<<<<< HEAD:GovServe-Project/Migrations/20260225141901_supervisor.cs
                name: "IX_Escalation_EscalatedByUserId",
                table: "Escalation",
                column: "EscalatedByUserId");

            migrationBuilder.CreateIndex(
========
>>>>>>>> 4f27f3ebb5d0a396421aee5dc4c6651122ec8e48:GovServe-Project/Migrations/20260224124537_p.cs
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
<<<<<<<< HEAD:GovServe-Project/Migrations/20260225141901_supervisor.cs
========
                name: "IX_SLARecords_ServiceID",
                table: "SLARecords",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_SLARecords_StageID",
                table: "SLARecords",
                column: "StageID");

            migrationBuilder.CreateIndex(
>>>>>>>> 4f27f3ebb5d0a396421aee5dc4c6651122ec8e48:GovServe-Project/Migrations/20260224124537_p.cs
                name: "IX_User_DepartmentId",
                table: "User",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStages_ServiceID",
                table: "WorkflowStages",
                column: "ServiceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
<<<<<<<< HEAD:GovServe-Project/Migrations/20260225141901_supervisor.cs
                name: "Appeal");

            migrationBuilder.DropTable(
========
>>>>>>>> 4f27f3ebb5d0a396421aee5dc4c6651122ec8e48:GovServe-Project/Migrations/20260224124537_p.cs
                name: "CitizenDocument");

            migrationBuilder.DropTable(
                name: "EligibilityRules");

            migrationBuilder.DropTable(
                name: "Escalation");
<<<<<<<< HEAD:GovServe-Project/Migrations/20260225141901_supervisor.cs

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "ServiceReports");
========

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "ServiceReports");

            migrationBuilder.DropTable(
                name: "SLARecords");

            migrationBuilder.DropTable(
                name: "Case");
>>>>>>>> 4f27f3ebb5d0a396421aee5dc4c6651122ec8e48:GovServe-Project/Migrations/20260224124537_p.cs

            migrationBuilder.DropTable(
                name: "WorkflowStages");

            migrationBuilder.DropTable(
<<<<<<<< HEAD:GovServe-Project/Migrations/20260225141901_supervisor.cs
                name: "Grievance");

            migrationBuilder.DropTable(
                name: "Case");

            migrationBuilder.DropTable(
========
>>>>>>>> 4f27f3ebb5d0a396421aee5dc4c6651122ec8e48:GovServe-Project/Migrations/20260224124537_p.cs
                name: "Application");

            migrationBuilder.DropTable(
                name: "RequiredDocuments");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
