namespace Application.DTOs.CompanyDtos
{
    public class CompanyProfileDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int JobCount { get; set; }
        public int OpenJobCount { get; set; }
    }
}
