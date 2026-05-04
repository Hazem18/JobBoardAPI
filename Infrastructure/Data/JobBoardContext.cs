using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Data
{
    public class JobBoardContext : IdentityDbContext<ApplicationUser>
    {
        public JobBoardContext(DbContextOptions<JobBoardContext> options)
       : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<JobListing> JobListings { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

     

            builder.Entity<JobListing>()
                .Property(j => j.SalaryMin)
                .HasPrecision(18, 2);

            builder.Entity<JobListing>()
                .Property(j => j.SalaryMax)
                .HasPrecision(18, 2);

            // fix cascade paths
            builder.Entity<JobApplication>()
                .HasOne(a => a.JobListing)
                .WithMany(j => j.JobApplications)
                .HasForeignKey(a => a.JobListingId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<JobApplication>()
                .HasOne(a => a.Candidate)
                .WithMany(c => c.JobApplications)
                .HasForeignKey(a => a.CandidateId)
                .OnDelete(DeleteBehavior.NoAction);
        }


    }
}
