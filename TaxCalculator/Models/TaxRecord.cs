using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.Models
{
    public class TaxRecord
    {
        [Key]
        public int Id { get; set; }
        public required int MunicipalityId { get; set; }
        public decimal TaxRate { get; set; }
        public DateTime StartDate { get; set; }
        public Priority TaxPrioritization { get; set; }
    }
    public enum Priority
    {
        Yearly = 4,
        Monthly = 3,
        Weekly = 2,
        Daily = 1
    }
}