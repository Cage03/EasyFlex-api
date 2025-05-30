using Interface.Interface.Dal;
using Interface.Dtos;
using Logic.Handlers;
using Moq;
using Interface.Models;

namespace Test.UnitTests.Handlers;

[TestClass]
public class FlexworkerHandlerTests
{
    private Mock<IFlexworkerDal> _mockFlexworkerDal = null!;
    private FlexworkerHandler _flexworkerHandler = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockFlexworkerDal = new Mock<IFlexworkerDal>();
        _flexworkerHandler = new FlexworkerHandler(_mockFlexworkerDal.Object);
    }

    [TestMethod]
    public async Task GetFlexWorkers_ShouldReturnPaginatedFlexWorkers()
    {
        // Arrange
        var flexworkers = new List<Flexworker>
        {
            new()
            {
                Id = 1, Name = "Flexworker1", Email = "email1@email.nl", Address = "Adress1",
                DateOfBirth = new DateOnly(1990, 10, 1), PhoneNumber = "0612345678", ProfilePictureUrl = "url1"
            },
            new()
            {
                Id = 2, Name = "Flexworker2", Email = "email2@email.nl", Address = "Adress2",
                DateOfBirth = new DateOnly(1990, 10, 1), PhoneNumber = "0612345678", ProfilePictureUrl = "url2"
            },
            new()
            {
                Id = 3, Name = "Flexworker3", Email = "email3@email.nl", Address = "Adress3",
                DateOfBirth = new DateOnly(1990, 10, 1), PhoneNumber = "0612345678", ProfilePictureUrl = "url3"
            },
        };

        int pageNumber = 1;
        int limit = 2;

        _mockFlexworkerDal
            .Setup(x => x.GetAllFlexworkers(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(flexworkers.Take(limit).Select(fw => new FlexworkerModel
            {
                Id = fw.Id,
                Name = fw.Name,
                Email = fw.Email,
                Address = fw.Address,
                DateOfBirth = fw.DateOfBirth,
                PhoneNumber = fw.PhoneNumber,
                ProfilePictureUrl = fw.ProfilePictureUrl,
            }).ToList());

        // Act
        var result = await _flexworkerHandler.GetFlexworkers(pageNumber, limit);

        // Assert
        Assert.AreEqual(2, result!.Count);
        Assert.AreEqual(1, result[0].Id);
        Assert.AreEqual(2, result[1].Id);
    }

    [TestMethod]
    public async Task GetFlexWorkers_ShouldReturnEmptyArrayIfNoFlexWorkersAvailable()
    {
        // Arrange
        var flexWorkers = new List<FlexworkerModel>(); // No flexworkers

        _mockFlexworkerDal.Setup(x => x.GetAllFlexworkers(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(flexWorkers);

        int pageNumber = 1;
        int limit = 2;

        // Act
        var result = await _flexworkerHandler.GetFlexworkers(pageNumber, limit);

        // Assert
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public async Task CreateFlexWorker_ShouldCreateFlexWorker()
    {
        // Arrange
        var flexworker = new Flexworker
        {
            Id = 1, Name = "Flexworker1", Email = "email1@email.nl", Address = "Adress1",
            DateOfBirth = new DateOnly(1990, 10, 1), PhoneNumber = "0612345678", ProfilePictureUrl = "url1"
        };

        _mockFlexworkerDal.Setup(x => x.AddFlexWorker(It.IsAny<FlexworkerModel>()));

        // Act
        await _flexworkerHandler.CreateFlexworker(flexworker);

        // Assert
        _mockFlexworkerDal.Verify(x => x.AddFlexWorker(It.IsAny<FlexworkerModel>()), Times.Once);
    }

    [TestMethod]
    public async Task CreateFlexWorker_ShouldThrowExceptionIfFlexWorkerAlreadyExists()
    {
        // Arrange
        var flexworker = new Flexworker
        {
            Id = 1, Name = "Flexworker1", Email = "email1@email.nl", Address = "Adress1",
            DateOfBirth = new DateOnly(1990, 10, 1), PhoneNumber = "0612345678", ProfilePictureUrl = "url1"
        };

        _mockFlexworkerDal.Setup(x => x.AddFlexWorker(It.IsAny<FlexworkerModel>()))
            .ThrowsAsync(new Exception("Flexworker already exists"));

        // Act & Assert
        var exception =
            await Assert.ThrowsExceptionAsync<Exception>(() => _flexworkerHandler.CreateFlexworker(flexworker));
        Assert.AreEqual("Flexworker already exists", exception.Message);
    }

    [TestMethod]
    public async Task DeleteFlexWorker_ShouldDeleteFlexWorker()
    {
        // Arrange
        var flexworker = new FlexworkerModel
        {
            Id = 1,
            Name = "Flexworker1",
            Email = "email1@email.nl",
            Address = "Adress1",
            DateOfBirth = new DateOnly(1990, 10, 1),
            PhoneNumber = "0612345678",
            ProfilePictureUrl = "url1"
        };

        _mockFlexworkerDal.Setup(x => x.GetFlexworkerById(It.IsAny<int>())).ReturnsAsync(flexworker);
        _mockFlexworkerDal.Setup(x => x.DeleteFlexworker(It.IsAny<int>()));

        // Act
        await _flexworkerHandler.DeleteFlexworker(flexworker.Id);

        // Assert
        _mockFlexworkerDal.Verify(x => x.DeleteFlexworker(It.IsAny<int>()), Times.Once);
    }

    [TestMethod]
    public async Task GetFlexworkerById_ShouldReturnFlexworker()
    {
        // Arrange
        var flexWorker = new FlexworkerModel
        {
            Id = 1, Name = "Flexworker1", Email = "email1@email.nl", Address = "Adress1",
            DateOfBirth = new DateOnly(1990, 10, 1), PhoneNumber = "0612345678", ProfilePictureUrl = "url1"
        };

        _mockFlexworkerDal.Setup(x => x.GetFlexworkerById(It.IsAny<int>())).ReturnsAsync(flexWorker);

        // Act
        var result = await _flexworkerHandler.GetFlexworkerById(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(flexWorker.Id, result.Id);
        Assert.AreEqual(flexWorker.Name, result.Name);
    }

    [TestMethod]
    public async Task UpdateFlexWorker_ShouldUpdateFlexWorker()
    {
        // Arrange
        var oldFlexWorker = new FlexworkerModel
        {
            Id = 1, Name = "Old_Flexworker", Email = "old@email.nl", Address = "Old_Adress",
            DateOfBirth = new DateOnly(1990, 10, 1), PhoneNumber = "0612345678", ProfilePictureUrl = "old_url"
        };
        var updatedFlexWorker = new Flexworker
        {
            Id = 1, Name = "New_Flexworker", Email = "new@email.nl", Address = "New_Adress",
            DateOfBirth = new DateOnly(2000, 11, 19), PhoneNumber = "new_0612345678", ProfilePictureUrl = "new_url"
        };

        _mockFlexworkerDal.Setup(x => x.GetFlexworkerById(It.IsAny<int>())).ReturnsAsync(oldFlexWorker);
        _mockFlexworkerDal.Setup(x => x.UpdateFlexworker(It.IsAny<FlexworkerModel>()));

        // Act
        await _flexworkerHandler.UpdateFlexworker(updatedFlexWorker);

        // Assert
        _mockFlexworkerDal.Verify(x => x.UpdateFlexworker(It.Is<FlexworkerModel>(
            fw => fw.Id == updatedFlexWorker.Id &&
                  fw.Name == updatedFlexWorker.Name &&
                  fw.Email == updatedFlexWorker.Email &&
                  fw.Address == updatedFlexWorker.Address &&
                  fw.PhoneNumber == updatedFlexWorker.PhoneNumber &&
                  fw.ProfilePictureUrl == updatedFlexWorker.ProfilePictureUrl
        )), Times.Once);
    }

    [TestMethod]
    public async Task UpdateFlexWorker_ShouldUpdateFlexWorker_WhenFlexWorkerExists()
    {
        // Arrange
        var existingFlexWorker = new FlexworkerModel
        {
            Id = 1, Name = "Old_Flexworker", Email = "old@email.nl", Address = "Old_Adress",
            DateOfBirth = new DateOnly(1990, 10, 1), PhoneNumber = "0612345678", ProfilePictureUrl = "old_url"
        };

        var updatedFlexWorker = new Flexworker
        {
            Id = 1, Name = "Updated_Flexworker", Email = "updated@email.nl", Address = "Updated_Adress",
            DateOfBirth = new DateOnly(2000, 11, 19), PhoneNumber = "New_0612345678", ProfilePictureUrl = "new_url"
        };

        _mockFlexworkerDal.Setup(x => x.GetFlexworkerById(It.IsAny<int>())).ReturnsAsync(existingFlexWorker);
        _mockFlexworkerDal.Setup(x => x.UpdateFlexworker(It.IsAny<FlexworkerModel>()));

        // Act
        await _flexworkerHandler.UpdateFlexworker(updatedFlexWorker);

        // Assert
        _mockFlexworkerDal.Verify(x => x.UpdateFlexworker(It.Is<FlexworkerModel>(
            fw => fw.Id == updatedFlexWorker.Id &&
                  fw.Name == updatedFlexWorker.Name &&
                  fw.Email == updatedFlexWorker.Email &&
                  fw.Address == updatedFlexWorker.Address &&
                  fw.PhoneNumber == updatedFlexWorker.PhoneNumber &&
                  fw.ProfilePictureUrl == updatedFlexWorker.ProfilePictureUrl
        )), Times.Once);
    }

    [TestMethod]
    public async Task AddSkills_ShouldAddSkillsToFlexWorker()
    {
        // Arrange
        var flexWorker = new FlexworkerModel
        {
            Id = 1, Name = "Flexworker1", Email = "email1@email.nl", Address = "Adress1",
            DateOfBirth = new DateOnly(1990, 10, 1), PhoneNumber = "0612345678", ProfilePictureUrl = "url1"
        };

        var skills = new List<Skill>
        {
            new Skill
            {
                Id = 1,
                CategoryId = 100,
                Name = "C#",
            },
            new Skill
            {
                Id = 2,
                CategoryId = 101,
                Name = "SQL",
            }
        };

        _mockFlexworkerDal.Setup(x => x.GetFlexworkerById(It.IsAny<int>())).ReturnsAsync(flexWorker);
        _mockFlexworkerDal.Setup(x => x.AddSkills(It.IsAny<int>(), It.IsAny<List<SkillModel>>()));
        // Act
        await _flexworkerHandler.AddSkills(flexWorker.Id, skills);

        // Assert
        _mockFlexworkerDal.Verify(x => x.AddSkills(It.IsAny<int>(), It.IsAny<List<SkillModel>>()), Times.Once);
    }

    [TestMethod]
    public async Task AddSkills_ShouldThrowExceptionIfNoSkillsProvided()
    {
        // Arrange
        var flexWorker = new FlexworkerModel
        {
            Id = 1, Name = "Flexworker1", Email = "email1@email.nl", Address = "Adress1",
            DateOfBirth = new DateOnly(1990, 10, 1), PhoneNumber = "0612345678", ProfilePictureUrl = "url1"
        };

        _mockFlexworkerDal.Setup(x => x.GetFlexworkerById(It.IsAny<int>())).ReturnsAsync(flexWorker);

        // Act & Assert
        var exception = await Assert.ThrowsExceptionAsync<Exception>(() =>
            _flexworkerHandler.AddSkills(flexWorker.Id, new List<Skill>()));

        Assert.AreEqual("No skills provided", exception.Message);

        _mockFlexworkerDal.Verify(x => x.AddSkills(It.IsAny<int>(), It.IsAny<List<SkillModel>>()),
            Times.Never);
    }

    [TestMethod]
    public async Task RemoveSkills_ShouldRemoveSkillsFromFlexWorker()
    {
        // Arrange
        var flexWorker = new FlexworkerModel
        {
            Id = 1, Name = "Flexworker1", Email = "email1@email.nl", Address = "Adress1",
            DateOfBirth = new DateOnly(1990, 10, 1), PhoneNumber = "0612345678", ProfilePictureUrl = "url1"
        };

        var skills = new List<Skill>
        {
            new Skill
            {
                Id = 1,
                CategoryId = 100,
                Name = "C#",
            },
            new Skill
            {
                Id = 2,
                CategoryId = 101,
                Name = "SQL",
            }
        };

        _mockFlexworkerDal.Setup(x => x.GetFlexworkerById(It.IsAny<int>())).ReturnsAsync(flexWorker);
        _mockFlexworkerDal.Setup(x => x.RemoveSkills(It.IsAny<int>(), It.IsAny<List<SkillModel>>()));

        // Act
        await _flexworkerHandler.RemoveSkills(flexWorker.Id, skills);

        // Assert
        _mockFlexworkerDal.Verify(x => x.RemoveSkills(It.IsAny<int>(), It.IsAny<List<SkillModel>>()),
            Times.Once);
    }

    [TestMethod]
    public async Task RemoveSkills_ShouldThrowExceptionIfNoSkillsProvided()
    {
        // Arrange
        // Arrange
        var flexWorker = new FlexworkerModel
        {
            Id = 1, Name = "Flexworker1", Email = "email1@email.nl", Address = "Adress1",
            DateOfBirth = new DateOnly(1990, 10, 1), PhoneNumber = "0612345678", ProfilePictureUrl = "url1"
        };

        _mockFlexworkerDal.Setup(x => x.GetFlexworkerById(It.IsAny<int>())).ReturnsAsync(flexWorker);

        // Act & Assert
        var exception = await Assert.ThrowsExceptionAsync<Exception>(() =>
            _flexworkerHandler.RemoveSkills(flexWorker.Id, new List<Skill>()));

        Assert.AreEqual("No skills provided", exception.Message);

    }
}