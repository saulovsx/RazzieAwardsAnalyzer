namespace RazzieAwardsAnalyzer.Domain.Interfaces.Repositories
{
    public interface IFileRepository
    {
        Task<List<string>> ReturnDataCsvFileAsync();
    }
}