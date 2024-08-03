using JobCandidateHub.DTOs;

namespace JobCandidateHub.Services
{
    public interface ICandidateService
    {
        Task AddOrUpdateCandidateAsync(CandidateDto candidateDto);
    }
}
