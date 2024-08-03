using JobCandidateHub.Configurations;
using JobCandidateHub.Models;
using Microsoft.EntityFrameworkCore;

namespace JobCandidateHub.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly CandidateContext _context;

        public CandidateRepository(CandidateContext context)
        {
            _context = context;
        }

        public async Task<Candidate> GetByEmailAsync(string email)
        {
            return await _context.Candidates.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task AddAsync(Candidate candidate)
        {
            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Candidate candidate)
        {
            _context.Candidates.Update(candidate);
            await _context.SaveChangesAsync();
        }
    }
}
