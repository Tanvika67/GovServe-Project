using GovServe_Project.DTOs;
using GovServe_Project.Services;
using Microsoft.AspNetCore.Mvc;

namespace GovServe_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowStagesController : ControllerBase
    {
        private readonly IWorkflowStageService _service;

        public WorkflowStagesController(IWorkflowStageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkflowStageDTO dto)
        {
            return Ok(await _service.CreateAsync(dto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, WorkflowStageDTO dto)
        {
            return Ok(await _service.UpdateAsync(id, dto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok("Deleted successfully.");
        }
    }
}

