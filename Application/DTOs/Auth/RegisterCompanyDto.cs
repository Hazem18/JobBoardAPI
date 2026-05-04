
namespace Application.DTOs.Auth
{
    public class RegisterCompanyDto
    {
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Website { get; set; }
        public string? Description { get; set; }
    }
}
