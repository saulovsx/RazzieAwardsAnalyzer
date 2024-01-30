using RazzieAwardsAnalyzer.Domain.Interfaces.Repositories;

namespace RazzieAwardsAnalyzer.Data.FileRepositories
{
    public sealed class FileRepository : IFileRepository
    {
        private readonly string _filePath = string.Empty;
        public FileRepository()
        {
            _filePath = Path.Combine(AppContext.BaseDirectory, "FileCSV");
        }
        public FileRepository(string filePath) => _filePath = filePath;

        public async Task<List<string>> ReturnDataCsvFileAsync()
        {
            try
            {                
                string? fileCSV = GetLatestCsvFile();
                _ = fileCSV ?? throw new FileNotFoundException();

                List<string> dataFile = [];
                using var sr = new StreamReader(fileCSV);
                string? line;
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    dataFile.Add(line);
                }
                return dataFile;
            }
            catch (IOException e)
            {
                throw new Exception($"O arquivo não pôde ser lido: {e.Message}");                
            }
        }        

        public string? GetLatestCsvFile()
        {
            var directoryInfo = new DirectoryInfo(_filePath);
            var csvFile = directoryInfo.GetFiles("*.csv")
                                       .OrderByDescending(f => f.LastWriteTime)
                                       .FirstOrDefault();

            return csvFile?.FullName;
        }
    }
}