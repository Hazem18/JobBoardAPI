using Domain.Enums;

namespace Domain.Entities
{
    public class JobListing
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public decimal SalaryMin { get; set; }
        public decimal SalaryMax { get; set; }
        public JobType JobType { get; set; } = JobType.FullTime;
        public JobStatus Status { get; set; } = JobStatus.Open;
        public DateTime PostedAt { get; set; }

        public string CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
    }
}
