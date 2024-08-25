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
        public DateTime? EndDate { get; set; }
    }
}