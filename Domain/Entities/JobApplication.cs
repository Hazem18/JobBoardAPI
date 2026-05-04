using Domain.Enums;
namespace Domain.Entities
{
    public class JobApplication
    {
        public int Id { get; set; }

        public string CoverLetter { get; set; }
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;

        public string CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        public int JobListingId { get; set; }
        public JobListing JobListing { get; set; }
        
    }
}
