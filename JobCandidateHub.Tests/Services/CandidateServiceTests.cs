using AutoMapper;
using JobCandidateHub.Configurations;
using JobCandidateHub.DTOs;
using JobCandidateHub.Mappings;
using JobCandidateHub.Models;
using JobCandidateHub.Repositories;
using JobCandidateHub.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JobCandidateHub.Tests.Services
{
    [TestClass]
    public class CandidateServiceTests
    {
        private CandidateService _candidateService;
        private CandidateContext _context;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<CandidateContext>()
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CandidateDB;Trusted_Connection=True;")
                .Options;

            _context = new CandidateContext(options);
            _context.Database.EnsureCreated();
            _context.Candidates.RemoveRange(_context.Candidates);
            _context.SaveChanges();

            var repository = new CandidateRepository(_context);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<CandidateProfile>());
            var mapper = config.CreateMapper();

            _candidateService = new CandidateService(repository, mapper);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Candidates.RemoveRange(_context.Candidates);
            _context.SaveChanges();
            _context.Dispose();
        }

        [TestMethod]
        public async Task AddOrUpdateCandidateAsync_ShouldAddCandidate_WhenCandidateDoesNotExist()
        {
            var candidateDto = new CandidateDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Comment = "Test Comment"
            };

            await _candidateService.AddOrUpdateCandidateAsync(candidateDto);

            var candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.Email == "john.doe@example.com");
            Assert.IsNotNull(candidate);
            Assert.AreEqual("John", candidate.FirstName);
        }

        [TestMethod]
        public async Task AddOrUpdateCandidateAsync_ShouldUpdateCandidate_WhenCandidateExists()
        {
            var existingCandidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Comment = "Old Comment"
            };
            _context.Candidates.Add(existingCandidate);
            _context.SaveChanges();

            var candidateDto = new CandidateDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Comment = "Updated Comment"
            };

            await _candidateService.AddOrUpdateCandidateAsync(candidateDto);

            var candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.Email == "john.doe@example.com");
            Assert.IsNotNull(candidate);
            Assert.AreEqual("Updated Comment", candidate.Comment);
        }
    }
}
