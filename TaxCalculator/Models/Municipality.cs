using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using TaxCalculator.Data;

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