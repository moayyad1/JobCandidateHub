using Microsoft.EntityFrameworkCore;
using JobCandidateHub.Models;

namespace JobCandidateHub.Configurations
{
    public class CandidateContext : DbContext
    {
        public CandidateContext(DbContextOptions<CandidateContext> options) : base(options) { }

        public DbSet<Candidate> Candidates { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>()
                .HasIndex(c => c.Email)
                .IsUnique();
        }
    }
}