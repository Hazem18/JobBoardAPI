
namespace Application.DTOs.JobListingDtos
{
    public class JobListingResponseDto
    {
        public int Id { get; set; }
        public string CompanyId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public decimal SalaryMin { get; set; }
        public decimal SalaryMax { get; set; }
        public string JobType { get; set; }
        public string Status { get; set; }
        public DateTime PostedAt { get; set; }
        public string CompanyName { get; set; }
        public int ApplicationCount { get; set; }
    }
}
