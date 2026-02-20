using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GovServe.Models;
using GovServe_Project.Data;

namespace GovServe_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasesController : ControllerBase
    {
        private readonly GovServe_ProjectContext _context;

        public CasesController(GovServe_ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Cases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Case>>> GetCase()
        {
            return await _context.Case.ToListAsync();
        }

        // GET: api/Cases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Case>> GetCase(int id)
        {
            var @case = await _context.Case.FindAsync(id);

            if (@case == null)
            {
                return NotFound();
            }

            return @case;
        }

        // PUT: api/Cases/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCase(int id, Case @case)
        {
            if (id != @case.CaseId)
            {
                return BadRequest();
            }

            _context.Entry(@case).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cases
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Case>> PostCase(Case @case)
        {
            _context.Case.Add(@case);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCase", new { id = @case.CaseId }, @case);
        }

        // DELETE: api/Cases/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCase(int id)
        {
            var @case = await _context.Case.FindAsync(id);
            if (@case == null)
            {
                return NotFound();
            }

            _context.Case.Remove(@case);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CaseExists(int id)
        {
            return _context.Case.Any(e => e.CaseId == id);
        }
    }
}
