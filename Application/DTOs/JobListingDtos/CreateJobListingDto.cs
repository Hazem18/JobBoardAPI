
namespace Application.DTOs.JobListingDtos
{
    public class CreateJobListingDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public decimal SalaryMin { get; set; }
        public decimal SalaryMax { get; set; }
        public string JobType { get; set; }
    }
}
