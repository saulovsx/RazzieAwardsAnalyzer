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

    }
}