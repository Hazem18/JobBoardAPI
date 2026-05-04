namespace Domain.Entities
{
    public class Candidate : ApplicationUser
    {
        public string ResumeUrl { get; set; }
        public string Skills { get; set; }
        public string Bio { get; set; }
        public ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
    }
}
