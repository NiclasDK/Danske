using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxCalculator.Data;
using TaxCalculator.Interfaces;
using TaxCalculator.Models;

namespace TaxCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipalitiesController : ControllerBase
    {
        private readonly IMunicipalityRepository _municipalityRepository;

        public MunicipalitiesController(IMunicipalityRepository municipalityRepository)
        {
            _municipalityRepository = municipalityRepository;
        }

        // GET: api/Municipalities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Municipality>>> Getmunicipalities()
        {
            var municipalitiesList = await _municipalityRepository.GetMunicipalitiesAsync();

            if (municipalitiesList == null)
            {
                NotFound();
            }

            return Ok(municipalitiesList);
        }

        // GET: api/Municipalities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Municipality>> GetMunicipality(int id)
        {
            var municipality = await _municipalityRepository.GetMunicipalityById(id);

            if (municipality == null)
            {
                return NotFound();
            }

            return Ok(municipality);
        }

        // GET: api/Municipalities/Copenhagen
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Municipality>> GetMunicipalityByName(string name)
        {
            var municipality = await _municipalityRepository.GetMunicipalityByName(name);

            if (municipality == null)
            {
                return NotFound();
            }

            return Ok(municipality);
        }

        // PUT: api/Municipalities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMunicipality(int id, Municipality municipality)
        {
            if (id != municipality.MunicipalityCode)
            {
                return BadRequest();
            }

            _municipalityRepository.SetEntryStateModified(municipality);

            try
            {
                await _municipalityRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MunicipalityExists(id))
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

        // POST: api/Municipalities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Municipality>> PostMunicipality(Municipality municipality)
        {
            await _municipalityRepository.Add(municipality);
            await _municipalityRepository.SaveChangesAsync();

            return CreatedAtAction("GetMunicipality", new { id = municipality.MunicipalityCode }, municipality);
        }

        // DELETE: api/Municipalities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMunicipality(int id)
        {
            var municipality = await _municipalityRepository.GetMunicipalityById(id);
            if (municipality == null)
            {
                return NotFound();
            }

            await _municipalityRepository.Remove(municipality);
            await _municipalityRepository.SaveChangesAsync();

            return NoContent();
        }

        private bool MunicipalityExists(int id)
        {
            return _municipalityRepository.MunicipalityExists(id);
        }
    }
}
