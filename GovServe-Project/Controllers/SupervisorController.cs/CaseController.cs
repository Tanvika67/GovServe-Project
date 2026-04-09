using GovServe_Project.DTOs.OfficerDTO;
using GovServe_Project.DTOs.SupervisorDTO;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Models.SuperModels;
using GovServe_Project.Repository.Interface.SuperRepositoryInterface;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Service_Implementation.SuperServiceImplementation;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using NuGet.Protocol.Core.Types;
using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;


namespace GovServe_Project.Controllers.SupervisorController.cs

{

    [EnableCors("AllowAll")]

    [ApiController]

    [Route("api/[controller]")]

    //[Authorize(Roles = "Supervisor")]
    public class CaseController : ControllerBase  {

    private readonly ICaseService _service;

    private readonly ICaseRepository _repository;

    public CaseController(ICaseService service, ICaseRepository repository)

    {

        _service = service;

        _repository = repository;

    }

    //POST only I can create a case; API will create a new case in the system   
     [HttpPost]

    //[Authorize(Roles = "Supervisor")]
    public async Task<IActionResult> CreateCase(CreateCaseDto dto)

    {

      var result = await _service.CreateCaseAsync(dto);

      return Ok(result);

}


//GET only I can see all cases//Fetches complete list of case from the database   
 [HttpGet("all")]

//[Authorize(Roles = "Supervisor")]
public async Task<IActionResult> GetAll()

{

    return Ok(await _service.GetAllCasesAsync());

}


//Returns only ongoing cases   
 [HttpGet("active")]

//[Authorize(Roles = "Supervisor")]
public async Task<IActionResult> GetActive()

{

    return Ok(await _service.GetActiveCasesAsync());

}


//Used to change status like: Assigned-->Inprogress-->Completed   
[HttpPut("update-status")]

//[Authorize(Roles = "Supervisor")]
public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusDto dto)

{

    var result = await _service.UpdateCaseStatus(dto.CaseId, dto.Status);

    return Ok(result);

}


//Returns cases where SLA time has already exceeded//I can easily identify delayed cases    
[HttpGet("sla-breached")]

//[Authorize(Roles = "Supervisor")]
public async Task<IActionResult> GetSLABreachedCases()

{

    var cases = await _service.GetSLABreachedCasesAsync();


    if (!cases.Any())

        return Ok(new { message = "No SLA breached cases" });


    return Ok(cases);

}


//GET Returns summary like Total cases;Active cases; SLA breached   
 [HttpGet("dashboard")]

//[Authorize(Roles = "Supervisor")]
public async Task<IActionResult> Dashboard()

{

    return Ok(await _service.GetDashboardAsync());

}


//POST to reassign a case to another officer//I can easily reassign a case to another officer if needed   
 [HttpPost("reassign-escalated")]

//[Authorize(Roles = "Supervisor")]
public async Task<IActionResult> ReassignEscalated(int caseId, int newOfficerId)

{

    var result = await _service.ReassignEscalatedCaseAsync(caseId, newOfficerId);

    return Ok(result);

}


//Supervisor,Officer,Grieviance needs this so I created this   
[HttpGet("case-details/{caseId}")]

public async Task<IActionResult> GetCaseDetails(int caseId)

{

    var result = await _service.GetCaseDetails(caseId);

    if (result == null)

        return NotFound();

    return Ok(result);

}

//For my dashboard I created this    
[HttpGet("officer-statistics")]

public async Task<IActionResult> GetOfficerStatistics()

{

    var result = await _service.GetOfficerStatisticsAsync();

    return Ok(result);

}

//To display on supervisor dashboard    
[HttpGet("dashboard-stats")]

public async Task<IActionResult> GetDashboardStats()

{

    var stats = await _service.GetDashboardStatsAsync();

    return Ok(stats);

}

//Officer work

[HttpGet("assigned/{officerId}")]

//[Authorize(Roles = "Officer")]
public async Task<IActionResult> GetAssignedCases(int officerId)

{

    var cases = await _service.GetAssignedCasesAsync(officerId);


    if (cases == null || !cases.Any())

        return NotFound(new { message = "No cases assigned to this officer." });


    return Ok(cases);

}


// Officer: View details of a specific case    
[HttpGet("{caseId}")]

//[Authorize(Roles = "Officer")]
public async Task<IActionResult> ViewCase(int caseId)

{

    var caseDetails = await _service.GetCaseByIdAsync(caseId);


    if (caseDetails == null)

        return NotFound(new { message = "Case not found." });


    return Ok(caseDetails);

}


// Officer: Approve a case    
[HttpPut("{caseId}/approve")]

//[Authorize(Roles = "Officer")]
public async Task<IActionResult> ApproveCase(int caseId)

{

    var result = await _service.ApproveCaseAsync(caseId);



    return Ok(new { message = "Case approved successfully." });

}


// Officer: Reject a case   
 [HttpPut("{caseId}/reject")]

//[Authorize(Roles = "Officer")]
public async Task<IActionResult> RejectCase(int caseId, [FromBody] string reason)

{

    var result = await _service.RejectCaseAsync(caseId, reason);
    return Ok(new { message = "Case rejected successfully." });

}


// Officer: Get resubmitted applications   
 [HttpGet("resubmitted/{officerId}")]

//[Authorize(Roles = "Officer")]
public async Task<IActionResult> GetResubmittedCases(int officerId)

{

    var cases = await _service.GetResubmittedCasesAsync(officerId);


    if (cases == null || !cases.Any())

        return NotFound(new { message = "No resubmitted cases found." });


    return Ok(cases);

}


// Officer: Dashboard summary (counts)    
[HttpGet("dashboard/{officerId}")]

//[Authorize(Roles = "Officer")]
public async Task<IActionResult> OfficerDashboard(int officerId)

{

    var summary = await _service.GetOfficerDashboardAsync(officerId);


    return Ok(summary);

}



[HttpGet("officer-detailed-dashboard/{officerId}")]

public async Task<IActionResult> GetDetailedDashboard(int officerId)

{

    var result = await _service.GetOfficerDetailedDashboardAsync(officerId);

    return Ok(result);

}



[HttpGet("cases-by-status/{officerId}/{status}")]

public async Task<IActionResult> GetCasesByStatus(int officerId, string status)

{

    var result = await _service.GetCasesByStatusAsync(officerId, status);

    return Ok(result);

}



[HttpGet("notifications/{officerId}")]

public async Task<IActionResult> GetNotifications(int officerId)

{

    var notifications = await _service.GetOfficerNotificationsAsync(officerId);


    if (notifications == null || !notifications.Any())

    {

        return Ok(new { message = "No new cases or SLA alerts." });

    }


    return Ok(notifications);

}


// PUT: api/Case/verify-documents/5    
[HttpPut("verify-documents/{caseId}")]

public async Task<IActionResult> VerifyDocuments(int caseId)

{

    // Logic for User Story 4
    var result = await _service.VerifyDocumentsAsync(caseId);


    if (result == "Case not found")

        return NotFound(new { message = result });


    return Ok(new { message = result });

}


	}

}
