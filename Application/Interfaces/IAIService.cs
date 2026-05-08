using Application.DTOs.AIDtos;

namespace Application.Interfaces
{
    public interface IAIService
    {
        Task<CoverLetterResponseDto> GenerateCoverLetterAsync(CoverLetterRequestDto dto);
        Task<MatchScoreResponseDto> GenerateMatchScoreAsync(MatchScoreRequestDto dto);
    }
}
