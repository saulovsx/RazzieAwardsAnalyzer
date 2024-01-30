using RazzieAwardsAnalyzer.Application.DTOs;
using RazzieAwardsAnalyzer.Application.Interfaces;
using RazzieAwardsAnalyzer.Domain.Entities;
using RazzieAwardsAnalyzer.Domain.Interfaces.Repositories;

namespace RazzieAwardsAnalyzer.Application.Services
{
    public class ImportFileService(IFileRepository fileRepository, IRazzieAwardRepository razzieAwardRepository) : IImportFileService
    {
        private readonly IFileRepository _fileRepository = fileRepository;
        private readonly IRazzieAwardRepository _razzieAwardRepository = razzieAwardRepository;
        public async Task ImportFileAsync()
        {
            try
            {
                var dataFile = await _fileRepository.ReturnDataCsvFileAsync();

                List<RazzieAwardCsvDTO> razzieAwardList = [];
                foreach (var line in dataFile)
                {
                    var dataLine = line.Split(';');
                    var razzieAward = ReturnRazzieAward(dataLine);
                    razzieAwardList.Add(razzieAward);
                }
                await CreateRazzieAward(razzieAwardList);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao importar arquivo: {ex.Message}");
            }            
        }

        private static RazzieAwardCsvDTO ReturnRazzieAward(string[] dataLine)
        {
            RazzieAwardCsvDTO razzieAward = new ()
            { 
                Year = dataLine[0],
                Title = dataLine[1],
                Studios = dataLine[2],
                Producers = dataLine[3],
                Winner = dataLine[4]                
            };
            return razzieAward;
        }

        private async Task CreateRazzieAward(List<RazzieAwardCsvDTO> razzieAwardList)
        {
            try
            {
                RemoveFirstTitleLine(razzieAwardList);

                var separator = new string[] { ",", " and " };
                var producers = razzieAwardList.SelectMany(f => f.Producers.Split(separator, StringSplitOptions.RemoveEmptyEntries))
                                               .Select(f => f.Trim())
                                               .Distinct()
                                               .Order();

                foreach (var producer in producers)
                    await _razzieAwardRepository.AddProducerAsync(new Producer(producer));
                await _razzieAwardRepository.ApplayChangesAsync();

                foreach (var item in razzieAwardList)
                {
                    List<Producer> producersMovie = [];

                    foreach (var producerName in item.Producers.Split(separator, StringSplitOptions.RemoveEmptyEntries))
                    {
                        Producer? producer = await _razzieAwardRepository.FindProducerByNameAsync(producerName.Trim());
                        if (producer != null)
                            producersMovie.Add(producer);
                    }

                    Movie movie = new()
                    {
                        Year = int.TryParse(item.Year, out var year) ? year : 0,
                        Title = item.Title,
                        Winner = item.Winner?.Trim() == "yes",
                        Producers = producersMovie
                    };
                    await _razzieAwardRepository.AddMovieAsync(movie);
                }
                await _razzieAwardRepository.ApplayChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao gravar dados: {ex.Message}");
            }
        }

        private static void RemoveFirstTitleLine(List<RazzieAwardCsvDTO> razzieAwardList) 
            => razzieAwardList.RemoveAt(0);
    }
}