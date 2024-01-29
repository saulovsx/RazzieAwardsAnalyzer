using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using RazzieAwardsAnalyzer.Application.Interfaces;
using RazzieAwardsAnalyzer.Application.Services;
using RazzieAwardsAnalyzer.Domain.Interfaces.Repositories;

namespace RazzieAwardsAnalyzer.Test
{
    public class ImportFileServiceTests
    {
        private readonly ImportFileService _importFileService;
        private readonly Mock<IFileRepository> _fileRepository;
        private readonly Mock<IRazzieAwardRepository> _razzieAwardRepository;

        private readonly string _filePath = string.Empty;

        public ImportFileServiceTests()
        {
            _fileRepository = new Mock<IFileRepository>();
            _razzieAwardRepository = new Mock<IRazzieAwardRepository>();
            _importFileService = new ImportFileService(_fileRepository.Object, _razzieAwardRepository.Object);

            _filePath = Path.Combine(AppContext.BaseDirectory, "FileCSV");
        }



    }
}
