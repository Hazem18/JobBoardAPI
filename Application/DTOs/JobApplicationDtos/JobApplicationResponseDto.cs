

namespace Application.DTOs.JobApplicationDtos
{
    public class JobApplicationResponseDto
    {
        public int Id { get; set; }
        public string CandidateId { get; set; }
        public string CoverLetter { get; set; }
        public string Status { get; set; }
        public DateTime AppliedAt { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string CandidateName { get; set; }
    }
}
