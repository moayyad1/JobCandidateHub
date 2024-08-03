using AutoMapper;
using JobCandidateHub.DTOs;
using JobCandidateHub.Models;

namespace JobCandidateHub.Mappings
{
    public class CandidateProfile : Profile
    {
        public CandidateProfile()
        {
            CreateMap<CandidateDto, Candidate>();
        }
    }
}
