using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxCalculator.Data;
using TaxCalculator.Interfaces;
using TaxCalculator.Models;

namespace TaxCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxRecordsController : ControllerBase
    {
        private readonly ITaxRecordsRepository _taxRecordsRepository;

        public TaxRecordsController(ITaxRecordsRepository taxRecordsRepository)
        {
            _taxRecordsRepository = taxRecordsRepository;
        }

        // GET: api/TaxRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaxRecord>>> GettaxRecords()
        {
            var taxrecords = await _taxRecordsRepository.GetTaxRecords();

            if (taxrecords == null)
            {
                return NotFound();
            }

            return Ok(taxrecords);
        }

        // GET: api/TaxRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaxRecord>> GetTaxRecord(int id)
        {
            var taxRecord = await _taxRecordsRepository.GetTaxRecord(id);

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

            _taxRecordsRepository.SetEntryStateModified(taxRecord);

            try
            {
                await _taxRecordsRepository.SaveChangesAsync();
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
            await _taxRecordsRepository.Add(taxRecord);
            await _taxRecordsRepository.SaveChangesAsync();

            return CreatedAtAction("GetTaxRecord", new { id = taxRecord.Id }, taxRecord);
        }

        // DELETE: api/TaxRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaxRecord(int id)
        {
            var taxRecord = await _taxRecordsRepository.GetTaxRecord(id);

            if (taxRecord == null)
            {
                return NotFound();
            }

            await _taxRecordsRepository.Remove(taxRecord);
            await _taxRecordsRepository.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Taxes/Copenhagen/2024-01-01
        [HttpGet("{municipalityName}/{date}")]
        public async Task<ActionResult<decimal>> GetTaxRate(string municipalityName, DateTime date)
        {
            var taxRate = await _taxRecordsRepository.FindMunicipalityTaxRateAtDate(municipalityName, date);

            //TODO might be a better way
            if (taxRate == null || taxRate == 0)
            {
                return NotFound("No tax rate found for the given date.");
            }

            return Ok(taxRate);
        }

        private bool TaxRecordExists(int id)
        {
            return _taxRecordsRepository.TaxRecordExists(id);
        }
    }
}
