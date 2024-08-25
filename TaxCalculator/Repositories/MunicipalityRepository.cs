using Microsoft.EntityFrameworkCore;
using TaxCalculator.Data;
using TaxCalculator.Interfaces;
using TaxCalculator.Models;

namespace TaxCalculator.Repositories
{
    public class MunicipalityRepository : IMunicipalityRepository
    {
        private readonly DataContext _dataContext;

        public MunicipalityRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Municipality>> GetMunicipalitiesAsync()
        {
            return await _dataContext.Municipalities.Include(m => m.TaxRecords).ToListAsync();
        }

        public async Task<Municipality> GetMunicipalityById(int id)
        {
            var municipality = await _dataContext.Municipalities.FindAsync(id);

            return municipality;
        }

        public async Task<Municipality> GetMunicipalityByName(string name)
        {
            var municipality = await _dataContext.Municipalities.Include(m => m.TaxRecords).FirstOrDefaultAsync(m => m.Name == name);

            return municipality;
        }

        public bool MunicipalityExists(int id)
        {
            return _dataContext.Municipalities.Any(e => e.MunicipalityCode == id);
        }

        public async Task SaveChangesAsync()
        {
            _dataContext.SaveChanges();
        }

        public async Task Add(Municipality municipality)
        {
            _dataContext.Add(municipality);
        }

        public async Task Remove(Municipality municipality)
        {
            _dataContext.Remove(municipality);
        }
        public void SetEntryStateModified(Municipality municipality)
        {
            _dataContext.Entry(municipality).State = EntityState.Modified;
        }

    }
}
