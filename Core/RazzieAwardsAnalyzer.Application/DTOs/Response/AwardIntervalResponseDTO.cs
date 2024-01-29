namespace RazzieAwardsAnalyzer.Application.DTOs.Response
{
    public record AwardIntervalResponseDTO
    {
        public List<ProducerIntervalDTO> Min { get; init; } = [];
        public List<ProducerIntervalDTO> Max { get; init; } = [];
    }

    public record ProducerIntervalDTO(string Producer, 
                                      int Interval, 
                                      int PreviousWin, 
                                      int FollowingWin)
    { }
}