namespace Domain.Entities
{
    public class Company : ApplicationUser
    {
        public string CompanyName { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }

        public ICollection<JobListing> JobListings { get; set; } = new List<JobListing>();

    }
}
