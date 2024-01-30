using RazzieAwardsAnalyzer.Domain.Entities;

namespace RazzieAwardsAnalyzer.Domain.Interfaces.Repositories
{
    public interface IRazzieAwardRepository
    {
        Task AddProducerAsync(Producer producer);
        Task<Producer?> FindProducerByNameAsync(string name);
        Task AddMovieAsync(Movie movie);
        Task<IEnumerable<Movie>> GetMoviesAsync();
        Task ApplayChangesAsync();
    }
}