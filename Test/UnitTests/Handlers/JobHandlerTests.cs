using DataAccess.Models;
using Interface.Interface.Dal;
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
            //Arrange
            JobModel job = new() { Id = 1, Name = "Job1", Address = "Address1", MinHours = 10, MaxHours = 20, StartDate = new DateOnly(2024, 10, 1) };
            _mockJobDal.Setup(x => x.CreateJob(It.IsAny<JobModel>())).ReturnsAsync(1);

            //Act
            var result = await _jobHandler.CreateJob(job);

            //Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task CreateJob_ShouldReturn0IfUnsuccessful()
        {
            //Arrange
            JobModel job = new() { Id = 1, Name = "Job1", Address = "Address1", MinHours = 10, MaxHours = 20, StartDate = new DateOnly(2024, 10, 1) };
            _mockJobDal.Setup(x => x.CreateJob(It.IsAny<JobModel>())).ReturnsAsync(0);

            //Act
            var result = await _jobHandler.CreateJob(job);

            //Assert
            Assert.AreEqual(0, result);
        }
        
        [TestMethod]
        public async Task DeleteJob_ShouldCallDalDeleteJob()
        {
            // Arrange
            int jobId = 1;
            _mockJobDal.Setup(x => x.DeleteJob(jobId)).Returns(Task.CompletedTask);

            // Act
            await _jobHandler.DeleteJob(jobId);

            // Assert
            _mockJobDal.Verify(x => x.DeleteJob(jobId), Times.Once);
        }

        [TestMethod]
        public async Task DeleteJob_ShouldNotCallDalIfInvalidId()
        {
            // Arrange
            int invalidJobId = -1;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<IndexOutOfRangeException>(async () => 
                await _jobHandler.DeleteJob(invalidJobId));
        }
          
        [TestMethod]  
        public async Task UpdateJob_ShouldBeSuccessful()
        {
            //Arrange
            JobModel job = new() { Id = 1, Name = "Job1", Address = "Address1", MinHours = 10, MaxHours = 20, StartDate = new DateOnly(2024, 10, 1) };
            _mockJobDal.Setup(x => x.UpdateJob(It.IsAny<JobModel>())).Returns(Task.CompletedTask);

            //Act
            await _jobHandler.UpdateJob(job);

            //Assert
            _mockJobDal.Verify(x => x.UpdateJob(job), Times.Once);
        }
        
        [TestMethod]
        public async Task UpdateJob_ShouldThrowExceptionIfUnsuccessful()
        {
            //Arrange
            JobModel job = new() { Id = 1, Name = "Job1", Address = "Address1", MinHours = 10, MaxHours = 20, StartDate = new DateOnly(2024, 10, 1) };
            _mockJobDal.Setup(x => x.UpdateJob(It.IsAny<JobModel>())).ThrowsAsync(new Exception());

            //Act
            async Task Act() => await _jobHandler.UpdateJob(job);

            //Assert
            await Assert.ThrowsExceptionAsync<Exception>(Act);
        }

        [TestMethod]
        public async Task GetJob_ShouldReturnJob()
        {
            //Arrange
            JobModel job = new() { Id = 1, Name = "Job1", Address = "Address1", MinHours = 10, MaxHours = 20, StartDate = new DateOnly(2024, 10, 1) };
            _mockJobDal.Setup(x => x.GetJob(It.IsAny<int>())).ReturnsAsync(job);

            //Act
            var result = await _jobHandler.GetJob(1);

            //Assert
            Assert.AreEqual(job, result);
        }

        [TestMethod]
        public async Task GetJob_ShouldReturnNullIfJobDoesNotExist()
        {
            //Arrange
            _mockJobDal.Setup(x => x.GetJob(It.IsAny<int>())).ReturnsAsync((JobModel?)null);

            //Act
            var result = await _jobHandler.GetJob(1);

            //Assert
            Assert.IsNull(result);
        }
    }
}
