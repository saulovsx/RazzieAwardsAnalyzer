namespace RazzieAwardsAnalyzer.Application.DTOs
{
    public record RazzieAwardCsvDTO
    {        
        public string Year { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public string Studios { get; init; } = string.Empty;
        public string Producers { get; init; } = string.Empty;
        public string? Winner { get; init; }

    }    
}