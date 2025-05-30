﻿using Interface.Dtos;
using Interface.Interface.Dal;
using Interface.Interface.Handlers;
using Interface.Models;
using Logic.Handlers;
using Moq;

namespace Test.UnitTests.Handlers;

[TestClass]
public class MatchingHandlerTests
{
    private Mock<IFlexworkerHandler> _mockFlexworkerHandler = null!;
    private Mock<IJobHandler> _mockJobHandler = null!;
    private MatchingHandler _matchingHandler = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockFlexworkerHandler = new Mock<IFlexworkerHandler>();
        _mockJobHandler = new Mock<IJobHandler>();
        _matchingHandler = new MatchingHandler(_mockFlexworkerHandler.Object, _mockJobHandler.Object);
    }

    //[TestMethod]
    //public async Task GetMatches_ShouldReturnMatchesForValidJobId()
    //{
    //    // Arrange
    //    var jobId = 1;
    //    var job = new Job
    //    { 
    //        Id = jobId, 
    //        Name = "Software Developer",
    //        Address = "123 Main St",
    //        MinHours = 30, 
    //        MaxHours = 40, 
    //        Preferences = new List<Preference>
    //        {
    //            new() { Id = 1, SkillId = 1, JobId = jobId, IsRequired = true, Weight = 10 },
    //            new() { Id = 2, SkillId = 2, JobId = jobId, IsRequired = false, Weight = 5 }
    //        }
    //    };

    //    var flexWorkers = new List<Flexworker>
    //    {
    //        new() 
    //        { 
    //            Id = 1, 
    //            Name = "Flexworker1", 
    //            Skills = new List<Skill> { new() { Id = 1, Name = "C#" }, new() { Id = 2, Name = "SQL" } }
    //        },
    //        new() 
    //        { 
    //            Id = 2, 
    //            Name = "Flexworker2", 
    //            Skills = new List<Skill> { new() { Id = 3, Name = "JavaScript" } }
    //        }
    //    };

    //    // Directly using Algorithm.FindFlexworkersForJob as it's static and cannot be mocked
    //    _mockJobHandler.Setup(x => x.GetJob(jobId)).ReturnsAsync(job);
    //    _mockFlexworkerHandler.Setup(x => x.GetAllFlexWorkers()).ReturnsAsync(flexWorkers);

    //    // Act
    //    var results = await _matchingHandler.GetMatchesForJob(jobId);

    //    // Assert
    //    Assert.IsNotNull(results);
    //    Assert.AreEqual(1, results.Count); // Only one worker should match based on preferences
    //    Assert.AreEqual(1, results[0].FlexworkerId); // Flexworker with Id 1 should match
    //}

    //[TestMethod]
    //public async Task GetMatches_ShouldThrowExceptionIfJobNotFound()
    //{
    //    // Arrange
    //    int invalidJobId = 999;

    //    _mockJobHandler.Setup(x => x.GetJob(invalidJobId)).ReturnsAsync((JobModel)null!);

    //    // Act & Assert
    //    await Assert.ThrowsExceptionAsync<NullReferenceException>(() => _matchingHandler.GetMatchesForJob(invalidJobId));
    //}

    //[TestMethod]
    //public async Task GetMatches_ShouldReturnEmptyListIfNoFlexWorkersAvailable()
    //{
    //    // Arrange
    //    var jobId = 1;
    //    var job = new JobModel { Id = jobId, Name = "Software Developer", MinHours = 30, MaxHours = 40 };
    //    var emptyFlexWorkers = new List<FlexworkerModel>();

    //    _mockJobHandler.Setup(x => x.GetJob(jobId)).ReturnsAsync(job);
    //    _mockFlexworkerHandler.Setup(x => x.GetAllFlexWorkers()).ReturnsAsync(emptyFlexWorkers);

    //    // Act
    //    var results = await _matchingHandler.GetMatchesForJob(jobId);

    //    // Assert
    //    Assert.IsNotNull(results);
    //    Assert.AreEqual(0, results.Count);
    //}

    //[TestMethod]
    //public async Task GetMatches_ShouldOnlyReturnCompatibleFlexworkers()
    //{
    //    // Arrange
    //    var jobId = 1;
    //    var job = new JobModel
    //    {
    //        Id = jobId,
    //        Name = "Data Analyst",
    //        Preferences = new List<PreferenceModel>
    //        {
    //            new() { Id = 1, SkillId = 1, JobId = jobId, IsRequired = true, Weight = 10 },
    //            new() { Id = 2, SkillId = 2, JobId = jobId, IsRequired = true, Weight = 5 }
    //        }
    //    };

    //    var flexWorkers = new List<FlexworkerModel>
    //    {
    //        new() 
    //        { 
    //            Id = 1, 
    //            Name = "QualifiedFlexworker", 
    //            Skills = new List<SkillModel> { new() { Id = 1, Name = "Python" }, new() { Id = 2, Name = "SQL" } } 
    //        },
    //        new() 
    //        { 
    //            Id = 2, 
    //            Name = "UnqualifiedFlexworker", 
    //            Skills = new List<SkillModel> { new() { Id = 3, Name = "Java" } }
    //        }
    //    };

    //    _mockJobHandler.Setup(x => x.GetJob(jobId)).ReturnsAsync(job);
    //    _mockFlexworkerHandler.Setup(x => x.GetAllFlexWorkers()).ReturnsAsync(flexWorkers);

    //    // Act
    //    var results = await _matchingHandler.GetMatchesForJob(jobId);

    //    // Assert
    //    Assert.IsNotNull(results);
    //    Assert.AreEqual(1, results.Count);  // Only the qualified flexworker should be in results
    //    Assert.AreEqual(1, results[0].FlexworkerId); // Flexworker with Id 1 should match
    //}
}
