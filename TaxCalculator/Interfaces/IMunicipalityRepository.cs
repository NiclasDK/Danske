using TaxCalculator.Models;

namespace TaxCalculator.Interfaces
{
    public interface IMunicipalityRepository
    {
        void SetEntryStateModified(Municipality municipality);
        bool MunicipalityExists(int id);
        Task<IEnumerable<Municipality>> GetMunicipalitiesAsync();
        Task<Municipality> GetMunicipalityById(int id);
        Task<Municipality> GetMunicipalityByName(string name);
        Task SaveChangesAsync();
        Task Add(Municipality municipality);
        Task Remove(Municipality municipality);
    }
}
