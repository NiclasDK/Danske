using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.Models
{
    public class Municipality
    {
        [Key]
        public int MunicipalityCode { get; set; }
        public string Name { get; set; }
        public ICollection<TaxRecord> TaxRecords { get; set; }
    }
}
