using JobCandidateHub.Models;

namespace JobCandidateHub.Repositories
{
    public interface ICandidateRepository
    {
        Task<Candidate> GetByEmailAsync(string email);
        Task AddAsync(Candidate candidate);
        Task UpdateAsync(Candidate candidate);
    }
}
