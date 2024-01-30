using RazzieAwardsAnalyzer.Data.FileRepositories;

namespace RazzieAwardsAnalyzer.Test
{
    public class FileRepositoryTests
    {
        private readonly FileRepository _repository;
        public FileRepositoryTests()
        {
            _repository = new FileRepository();
        }

        [Fact]
        public void Verify_File_Exisits()
        {
            var pathFile = _repository.GetLatestCsvFile();
            Assert.NotNull(pathFile);
        }

        [Fact]
        public async Task Verify_File_Data_Structure()
        {
            var dataFile = await _repository.ReturnDataCsvFileAsync();
            var dataLine = dataFile[0].Split(';');

            Assert.True(dataLine[0] == "year" && dataLine[1] == "title" 
                                              && dataLine[2] == "studios" 
                                              && dataLine[3] == "producers" 
                                              && dataLine[4] == "winner");
        }
    }
}