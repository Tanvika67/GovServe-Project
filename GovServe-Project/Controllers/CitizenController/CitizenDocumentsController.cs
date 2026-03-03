using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GovServe_Project.Data;
using GovServe_Project.DTOs;
using GovServe_Project.Extensions;
using GovServe_Project.Models;
using GovServe_Project.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GovServe_Project.Services.Service_Implementation.CitizenService_Implementation;
using GovServe_Project.Services.Interfaces.CitizenService_Interface;
using GovServe_Project.DTOs.CitizenDTO;
using Microsoft.AspNetCore.Authorization;


namespace GovServe_Project.Controllers.CitizenController
{
	[Route("api/[controller]")]
	[ApiController]
	
	public class CitizenDocumentController : ControllerBase
	{
		private readonly ICitizenDocumentService _service;

		public CitizenDocumentController(ICitizenDocumentService service)
		{
			_service = service;
		}

		// Upload Document
		[HttpPost("upload")]
		//[Authorize(Roles = "Citizen")]
		public async Task<IActionResult> UploadDocument([FromForm] UploadCitizenDocumentDTO dto)
		{
			var result = await _service.UploadDocumentAsync(dto);
			return Ok(result);
		}

		// Document Status
		[HttpGet("status/{id}")]
		[Authorize(Roles = "Citizen")]
		public async Task<IActionResult> GetDocumentStatus(int id)
		{
			var status = await _service.GetDocumentStatusAsync(id);

			if (status == null)
				return NotFound("Document Not Found");

			return Ok(status);
		}

		// Delete Document
		[HttpDelete("delete/{id}")]
		[Authorize(Roles = "Citizen")]
		public async Task<IActionResult> DeleteDocument(int id)
		{
			var result = await _service.DeleteDocumentAsync(id);

			if (!result)
				return NotFound("Document Not Found");

			return Ok("Document Deleted Successfully");
		}

		[HttpPut("ApproveDocument/{CitizenDocumentID}")]
		[Authorize(Roles = "Officer")]
		public async Task<IActionResult> ApproveDocument(int id)
		{
			var result = await _service.ApproveDocument(id);
			return Ok(result);
		}

		[HttpPut("RejectDocument/{id}")]
		[Authorize(Roles = "Officer")]
		public async Task<IActionResult> RejectDocument(int id)
		{
			var result = await _service.RejectDocument(id);
			return Ok(result);
		}
	}
}
