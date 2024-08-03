using AutoMapper;
using JobCandidateHub.DTOs;
using JobCandidateHub.Models;
using JobCandidateHub.Repositories;

namespace JobCandidateHub.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;

        public CandidateService(ICandidateRepository candidateRepository, IMapper mapper)
        {
            _candidateRepository = candidateRepository;
            _mapper = mapper;
        }

        public async Task AddOrUpdateCandidateAsync(CandidateDto candidateDto)
        {
            var candidate = await _candidateRepository.GetByEmailAsync(candidateDto.Email);
            if (candidate == null)
            {
                candidate = _mapper.Map<Candidate>(candidateDto);
                await _candidateRepository.AddAsync(candidate);
            }
            else
            {
                _mapper.Map(candidateDto, candidate);
                await _candidateRepository.UpdateAsync(candidate);
            }
        }
    }
}
