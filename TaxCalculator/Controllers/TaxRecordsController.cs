using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxCalculator.Data;
using TaxCalculator.Models;

namespace TaxCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxRecordsController : ControllerBase
    {
        private readonly DataContext _context;

        public TaxRecordsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TaxRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaxRecord>>> GetTaxRecords()
        {
            return await _context.TaxRecords.ToListAsync();
        }

        // GET: api/TaxRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaxRecord>> GetTaxRecord(int id)
        {
            var taxRecord = await _context.TaxRecords.FindAsync(id);

            if (taxRecord == null)
            {
                return NotFound();
            }

            return taxRecord;
        }

        // PUT: api/TaxRecords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaxRecord(int id, TaxRecord taxRecord)
        {
            if (id != taxRecord.Id)
            {
                return BadRequest();
            }

            _context.Entry(taxRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaxRecordExists(id))
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

        // POST: api/TaxRecords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaxRecord>> PostTaxRecord(TaxRecord taxRecord)
        {
            _context.TaxRecords.Add(taxRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaxRecord", new { id = taxRecord.Id }, taxRecord);
        }

        // DELETE: api/TaxRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaxRecord(int id)
        {
            var taxRecord = await _context.TaxRecords.FindAsync(id);
            if (taxRecord == null)
            {
                return NotFound();
            }

            _context.TaxRecords.Remove(taxRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaxRecordExists(int id)
        {
            return _context.TaxRecords.Any(e => e.Id == id);
        }
    }
}
