using RazzieAwardsAnalyzer.Application.DTOs.Response;

namespace RazzieAwardsAnalyzer.Application.Interfaces
{
    public interface IAwardIntervalService
    {
        Task<AwardIntervalResponseDTO> GetAwardInterval();
    }
}