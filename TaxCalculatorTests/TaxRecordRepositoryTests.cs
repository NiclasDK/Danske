using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using TaxCalculator.Data;
using TaxCalculator.Models;
using TaxCalculator.Repositories;

namespace TaxCalculatorTests
{
    public class TaxRecordRepositoryTests
    {
        private readonly TaxRecordRepository _taxRecordRepository;
        private readonly DataContext _dataContext;

        public TaxRecordRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("TaxRecordDb")
                .Options;

            _dataContext = new DataContext(options);
            _taxRecordRepository = new TaxRecordRepository(_dataContext);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            if (!_dataContext.Municipalities.Any())
            {
                var municipality = new Municipality { MunicipalityCode = 1000, Name = "Copenhagen" };
                var taxRecord = new TaxRecord
                {
                    Id = 1,
                    MunicipalityId = municipality.MunicipalityCode,
                    TaxRate = 0.2m,
                    StartDate = new DateTime(2024, 1, 1),
                    EndDate = new DateTime(2024, 12, 31),
                    TaxPrioritization = Priority.Weekly
                };

                var municipality2 = new Municipality { MunicipalityCode = 2000, Name = "Frederiksberg" };
                var taxRecord2 = new TaxRecord
                {
                    Id = 2,
                    MunicipalityId = municipality.MunicipalityCode,
                    TaxRate = 0.1m,
                    StartDate = new DateTime(2024, 1, 1),
                    EndDate = new DateTime(2024, 12, 31),
                    TaxPrioritization = Priority.Daily
                };

                var taxRecord3 = new TaxRecord
                {
                    Id = 3,
                    MunicipalityId = municipality.MunicipalityCode,
                    TaxRate = 0.3m,
                    StartDate = new DateTime(2024, 1, 1),
                    EndDate = new DateTime(2024, 12, 31),
                    TaxPrioritization = Priority.Yearly
                };

                _dataContext.Municipalities.Add(municipality);
                _dataContext.Municipalities.Add(municipality2);
                _dataContext.TaxRecords.Add(taxRecord);
                _dataContext.TaxRecords.Add(taxRecord2);
                _dataContext.TaxRecords.Add(taxRecord3);
                _dataContext.SaveChanges();
            }
        }

        [Fact]
        public async Task GetTaxRecord_ReturnsTaxRecord_WhenExists()
        {
            // Act
            var result = await _taxRecordRepository.GetTaxRecord(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0.2m, result.TaxRate);
        }

        [Fact]
        public async Task Add_AddsTaxRecordSuccessfully()
        {
            var taxRecord4 = new TaxRecord
            {
                Id = 4,
                MunicipalityId = 5000,
                TaxRate = 0.2m,
                StartDate = new DateTime(2024, 1, 1),
                EndDate = new DateTime(2024, 12, 31),
                TaxPrioritization = Priority.Yearly
            };
            // Act
            await _taxRecordRepository.Add(taxRecord4);
            _taxRecordRepository.SaveChangesAsync();

            // Assert
            Assert.Equal(4, _taxRecordRepository.GetTaxRecords().Result.Count());
        }

        [Fact]
        public async Task FindMunicipalityTaxRateAtDate_ReturnsCorrectTaxRate()
        {
            // Act
            var result = await _taxRecordRepository.FindMunicipalityTaxRateAtDate("Copenhagen", new DateTime(2024, 8, 25));

            // Assert
            Assert.Equal(0.1m, result);
        }
    }
}