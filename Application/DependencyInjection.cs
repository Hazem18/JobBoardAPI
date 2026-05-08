using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IJobListingService, JobListingService>();
            services.AddScoped<IJobApplicationService, JobApplicationService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IAIService, GeminiService>();
            return services;
        }
    }
}
