using Microsoft.EntityFrameworkCore;
using RazzieAwardsAnalyzer.Data.Context;
using RazzieAwardsAnalyzer.Domain.Entities;
using RazzieAwardsAnalyzer.Domain.Interfaces.Repositories;

namespace RazzieAwardsAnalyzer.Data.DataRepositories
{
    public class RazzieAwardRepository(ApplicationDbContext context) : IRazzieAwardRepository
    {
        private readonly ApplicationDbContext _dbContext = context;

        public async Task AddProducerAsync(Producer producer)
        {
            await _dbContext.AddAsync(producer);            
        }
        public async Task<Producer?> FindProducerByNameAsync(string name)
        {
            return await _dbContext.Producers.FirstOrDefaultAsync(s => s.Name == name);
        }
        public async Task AddMovieAsync(Movie movie)
        {
            await _dbContext.AddAsync(movie);            
        }
        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {
            var movies = await _dbContext.Movies
                                   .Include(m => m.Producers)
                                   .AsNoTracking()
                                   .ToListAsync();
            return movies;
        }

        public async Task ApplayChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}