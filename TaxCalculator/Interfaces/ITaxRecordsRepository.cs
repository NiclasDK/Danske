using TaxCalculator.Models;

namespace TaxCalculator.Interfaces
{
    public interface ITaxRecordsRepository
    {
        void SetEntryStateModified(TaxRecord taxRecord);
        bool TaxRecordExists(int id);
        Task<TaxRecord> GetTaxRecord(int id);
        Task<IEnumerable<TaxRecord>> GetTaxRecords();
        Task SaveChangesAsync();
        Task Add(TaxRecord taxRecord);
        Task Remove(TaxRecord taxRecord);
        Task<decimal> FindMunicipalityTaxRateAtDate(string municipalityName, DateTime date);
    }
}
