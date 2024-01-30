using Microsoft.EntityFrameworkCore;
using RazzieAwardsAnalyzer.Application.Services;
using RazzieAwardsAnalyzer.Data.Context;
using RazzieAwardsAnalyzer.Data.DataRepositories;
using RazzieAwardsAnalyzer.Data.FileRepositories;

namespace RazzieAwardsAnalyzer.Test
{
    public class ImportFileServiceTests
    {
        public ImportFileServiceTests()
        {}

        [Fact]
        public async Task Check_Imported_Data() 
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                              .UseInMemoryDatabase(databaseName: "RazzieAwardsDBTest")
                              .Options;
            using var context = new ApplicationDbContext(options);

            var _razzieAwardRepository = new RazzieAwardRepository(context);
            var fileRepository = new FileRepository(Path.Combine(AppContext.BaseDirectory, "FileCSV"));
            var _importFileService = new ImportFileService(fileRepository, _razzieAwardRepository);

            //Act
            await _importFileService.ImportFileAsync();
            var importedData = context.Movies.ToList();

            //Assert
            Assert.NotEmpty(importedData);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task Check_Int_Year_Data_Consistency()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                              .UseInMemoryDatabase(databaseName: "RazzieAwardsDBTest")
                              .Options;
            using var context = new ApplicationDbContext(options);

            var _razzieAwardRepository = new RazzieAwardRepository(context);
            var fileRepository = new FileRepository(Path.Combine(AppContext.BaseDirectory, "FileCSV"));
            var _importFileService = new ImportFileService(fileRepository, _razzieAwardRepository);

            //Act
            await _importFileService.ImportFileAsync();
            var importedData = context.Movies.FirstOrDefault();

            //Assert
            Assert.True(IsValidYear(importedData?.Year));
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task Check_Bool_Winner_Data_Consistency()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                              .UseInMemoryDatabase(databaseName: "RazzieAwardsDBTest")
                              .Options;
            using var context = new ApplicationDbContext(options);

            var _razzieAwardRepository = new RazzieAwardRepository(context);
            var fileRepository = new FileRepository(Path.Combine(AppContext.BaseDirectory, "FileCSV"));
            var _importFileService = new ImportFileService(fileRepository, _razzieAwardRepository);

            //Act
            await _importFileService.ImportFileAsync();
            var importedData = context.Movies.ToList();
            int countWinnerFalse = importedData.Where(x => x.Winner == false).Count();
            int countWinnerTrue  = importedData.Where(x => x.Winner == true).Count();
            //Assert
            Assert.True(countWinnerFalse > 0 && countWinnerTrue > 0);
        }
        private static bool IsValidYear(int? year)
        {
            try
            {
                if(year == null)
                    return false;

                DateTime date = new (year.Value, 1, 1);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}