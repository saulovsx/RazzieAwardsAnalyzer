using RazzieAwardsAnalyzer.Data.FileRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
