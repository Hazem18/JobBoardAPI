namespace Application.DTOs.CandidateDtos
{
    public class CandidateProfileDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Skills { get; set; } 
        public string Bio { get; set; }
        public string ResumeUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
