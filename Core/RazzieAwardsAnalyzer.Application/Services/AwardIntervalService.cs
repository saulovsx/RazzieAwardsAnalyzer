using RazzieAwardsAnalyzer.Application.DTOs.Response;
using RazzieAwardsAnalyzer.Application.Interfaces;
using RazzieAwardsAnalyzer.Domain.Entities;
using RazzieAwardsAnalyzer.Domain.Interfaces.Repositories;

namespace RazzieAwardsAnalyzer.Application.Services
{
    public class AwardIntervalService(IRazzieAwardRepository razzieAwardRepository) : IAwardIntervalService
    {
        private readonly IRazzieAwardRepository _razzieAwardRepository = razzieAwardRepository;

        public async Task<AwardIntervalResponseDTO> GetAwardInterval()
        {
            var movies = await _razzieAwardRepository.GetMoviesAsync();

            var producerIntervals = GetProducerYearData(movies);
            var minIntervals = GetMinIntervals(producerIntervals);
            var maxIntervals = GetMaxIntervals(producerIntervals);

            var awardIntervalResponseDTO = new AwardIntervalResponseDTO
            {
                Min = minIntervals,
                Max = maxIntervals
            };
            return awardIntervalResponseDTO;
        }

        private static List<(string Producer, int Year)> GetProducerYearData(IEnumerable<Movie> movies)
        {
            var producerYearData = new List<(string Producer, int Year)>();

            foreach (var movie in movies)
            {
                if (movie.Winner)
                {
                    foreach (var producer in movie.Producers)
                        producerYearData.Add((producer.Name ?? string.Empty, movie.Year));
                }
            }

            return producerYearData.OrderBy(p => p.Producer).ThenBy(p => p.Year).ToList();
        }

        private static List<ProducerIntervalDTO> GetMinIntervals(List<(string Producer, int Year)> producerYearData)
        {
            List<ProducerIntervalDTO> producerIntervals = CalculateIntervals(producerYearData);
            if(producerIntervals.Count == 0)
                return producerIntervals;

            int minInterval = producerIntervals.Where(i => i.Interval > 0)
                                               .Min(i => i.Interval);

            return producerIntervals.Where(i => i.Interval == minInterval)
                                    .ToList();
        }
        private static List<ProducerIntervalDTO> GetMaxIntervals(List<(string Producer, int Year)> producerYearData)
        {
            List<ProducerIntervalDTO> producerIntervals = CalculateIntervals(producerYearData);
            if (producerIntervals.Count == 0)
                return producerIntervals;

            int maxInterval = producerIntervals.Max(i => i.Interval);

            return producerIntervals.Where(i => i.Interval == maxInterval)
                                    .ToList();
        }
        private static List<ProducerIntervalDTO> CalculateIntervals(List<(string Producer, int Year)> producerYearData)
        {
            return producerYearData
                .GroupBy(p => p.Producer)
                .SelectMany(g =>
                {
                    List<int> years = g.Select(x => x.Year).OrderBy(y => y).ToList();
                    List<ProducerIntervalDTO> intervals = [];

                    for (int i = 0; i < years.Count - 1; i++)
                    {
                        intervals.Add(new ProducerIntervalDTO(g.Key, years[i + 1] - years[i], years[i], years[i + 1]));
                    }

                    return intervals;
                }).ToList();
        }
    }
}