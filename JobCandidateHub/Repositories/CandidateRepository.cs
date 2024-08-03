using JobCandidateHub.Configurations;
using JobCandidateHub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace JobCandidateHub.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly CandidateContext _context;
        private readonly IMemoryCache _cache;

        public CandidateRepository(CandidateContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        } 
      
        public async Task<Candidate> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

            Candidate candidate;
            if (!_cache.TryGetValue(email, out candidate))
            {
                candidate = await _context.Candidates.SingleOrDefaultAsync(c => c.Email == email);

                if (candidate != null)
                {
                    _cache.Set(email, candidate, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(25)
                    });
                }
            }

            return candidate;
        }

        public async Task AddAsync(Candidate candidate)
        {
            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();

            _cache.Set(candidate.Email, candidate, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(25)
            });
        }

        public async Task UpdateAsync(Candidate candidate)
        {
            _context.Candidates.Update(candidate);
            await _context.SaveChangesAsync();

            _cache.Set(candidate.Email, candidate, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(25)
            });
        }
    }
}
