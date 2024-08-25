using Microsoft.EntityFrameworkCore;
using TaxCalculator.Data;
using TaxCalculator.Interfaces;
using TaxCalculator.Models;

namespace TaxCalculator.Repositories
{
    public class TaxRecordRepository : ITaxRecordsRepository
    {

        private readonly DataContext _dataContext;

        public TaxRecordRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Add(TaxRecord taxRecord)
        {
            _dataContext.TaxRecords.Add(taxRecord);

        }

        public async Task<decimal> FindMunicipalityTaxRateAtDate(string municipalityName, DateTime date)
        {
            decimal taxRate = await _dataContext.Municipalities
                .Where(m => m.Name == municipalityName)
                .SelectMany(m => m.TaxRecords)
                .Where(tr => tr.StartDate <= date && tr.EndDate >= date)
                .OrderBy(tr => tr.TaxPrioritization)
                .Select(tr => tr.TaxRate)
                .FirstOrDefaultAsync();

            return taxRate;
        }

        public async Task<TaxRecord> GetTaxRecord(int id)
        {
            return await _dataContext.TaxRecords.FindAsync(id);
        }

        public async Task<IEnumerable<TaxRecord>> GetTaxRecords()
        {
            return await _dataContext.TaxRecords.ToListAsync();
        }

        public async Task Remove(TaxRecord taxRecord)
        {
            _dataContext.Remove(taxRecord);
        }

        public async Task SaveChangesAsync()
        {
            _dataContext.SaveChanges();
        }

        public void SetEntryStateModified(TaxRecord taxRecord)
        {
            _dataContext.Entry(taxRecord).State = EntityState.Modified;
        }

        public bool TaxRecordExists(int id)
        {
            return _dataContext.TaxRecords.Any(e => e.Id == id);
        }
    }
}
