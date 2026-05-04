
namespace Application.DTOs.Auth
{
    public class RegisterCandidateDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Skills { get; set; }
        public string? Bio { get; set; }
        public string? ResumeUrl { get; set; }
    }
}
