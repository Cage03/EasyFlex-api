using Interface.Interface.Dal;
using Interface.Models;
using Logic.Handlers;
using Moq;

namespace Test.UnitTests.Handlers
{
    [TestClass]
    public class JobHandlerTests
    {
        private Mock<IJobDal> _mockJobDal = null!;
        private JobHandler _jobHandler = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockJobDal = new Mock<IJobDal>();
            _jobHandler = new JobHandler(_mockJobDal.Object);
        }

        [TestMethod]
        public async Task GetJobs_ShouldReturnPaginatedJobs()
        {
            // Arrange
            var jobs = new List<JobModel>
            {
                new() { Id = 1, Name = "Job1", Address = "Address1", MinHours = 10, MaxHours = 20, StartDate = new DateOnly(2024, 10, 1) },
                new() { Id = 2, Name = "Job2", Address = "Address2", MinHours = 15, MaxHours = 25, StartDate = new DateOnly(2024, 10, 2) },
                new() { Id = 3, Name = "Job3", Address = "Address3", MinHours = 20, MaxHours = 30, StartDate = new DateOnly(2024, 10, 3) }
            };

            int pageNumber = 1;
            int limit = 2;
            
            // ReSharper disable once UselessBinaryOperation
            var offset = (pageNumber - 1) * limit;
            
            _mockJobDal
                .Setup(x => x.GetJobs(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(jobs.Skip(offset).Take(limit).ToList());

            // Act
            var result = await _jobHandler.GetJobs(pageNumber, limit);

            // Assert
            Assert.AreEqual(2, result!.Length);
            Assert.AreEqual(1, result[0].Id); 
            Assert.AreEqual(2, result[1].Id); 
        }

        [TestMethod]
        public async Task GetJobs_ShouldReturnEmptyArrayIfNoJobsAvailable()
        {
            // Arrange
            var jobs = new List<JobModel>(); // No jobs

            _mockJobDal.Setup(x => x.GetJobs(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(jobs);

            int pageNumber = 1;
            int limit = 2;

            // Act
            var result = await _jobHandler.GetJobs(pageNumber, limit);

            // Assert
            Assert.AreEqual(0, result!.Length);
        }

        [TestMethod]
        public async Task CreateJob_ShouldReturn1IfSuccessful ()
        {
            JobModel job = new() { Id = 1, Name = "Job1", Address = "Address1", MinHours = 10, MaxHours = 20, StartDate = new DateOnly(2024, 10, 1) };
            _mockJobDal.Setup(x => x.CreateJob(It.IsAny<JobModel>())).ReturnsAsync(1);
            var result = await _jobHandler.CreateJob(job);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task CreateJob_ShouldReturn0IfUnsuccessful()
        {
            JobModel job = new() { Id = 1, Name = "Job1", Address = "Address1", MinHours = 10, MaxHours = 20, StartDate = new DateOnly(2024, 10, 1) };
            _mockJobDal.Setup(x => x.CreateJob(It.IsAny<JobModel>())).ReturnsAsync(0);
            var result = await _jobHandler.CreateJob(job);
            Assert.AreEqual(0, result);
        }
    }
}
