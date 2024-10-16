using Interface.Interface.Dal;
using Interface.Models;
using Logic.Handlers;
using Moq;

namespace Test.UnitTests.Handlers;

[TestClass]
public class FlexworkerHandlerTests
{
    private Mock<IFlexWorkerDal> _mockFlexworkerDal = null!;
    private FlexWorkerHandler _flexWorkerHandler = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockFlexworkerDal = new Mock<IFlexWorkerDal>();
        _flexWorkerHandler = new FlexWorkerHandler(_mockFlexworkerDal.Object);
    }

    [TestMethod]
    public async Task GetFlexWorkers_ShouldReturnPaginatedFlexWorkers()
    {
        // Arrange
        var flexWorkers = new List<FlexworkerModel>
        {
            new()
            {
                Id = 1, Name = "Flexworker1", Email = "email1@email.nl", Adress = "Adress1",
                DateOfBirth = new DateTime(1990, 10, 1), PhoneNumber = "0612345678", ProfilePictureUrl = "url1"
            },
            new()
            {
                Id = 2, Name = "Flexworker2", Email = "email2@email.nl", Adress = "Adress2",
                DateOfBirth = new DateTime(1990, 10, 1), PhoneNumber = "0612345678", ProfilePictureUrl = "url1"
            },
            new()
            {
                Id = 3, Name = "Flexworker3", Email = "email3@email.nl", Adress = "Adress3",
                DateOfBirth = new DateTime(1990, 10, 1), PhoneNumber = "0612345678", ProfilePictureUrl = "url1"
            },
        };

        int pageNumber = 1;
        int limit = 2;

        var offset = (pageNumber - 1) * limit;

        _mockFlexworkerDal
            .Setup(x => x.GetAllFlexWorkers(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(flexWorkers.Skip(offset).Take(limit).ToList());
        // Act
        var result = await _flexWorkerHandler.GetFlexWorkers(pageNumber, limit);

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

        _mockFlexworkerDal.Setup(x => x.GetAllFlexWorkers(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(flexWorkers);

        int pageNumber = 1;
        int limit = 2;

        // Act
        var result = await _flexWorkerHandler.GetFlexWorkers(pageNumber, limit);

        // Assert
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public async Task CreateFlexWorker_ShouldCreateFlexWorker()
    {
        // Arrange
        var flexWorker = new FlexworkerModel
        {
            Id = 1, Name = "Flexworker1", Email = "email1@email.nl", Adress = "Adress1",
            DateOfBirth = new DateTime(1990, 10, 1), PhoneNumber = "0612345678", ProfilePictureUrl = "url1"
        };

        _mockFlexworkerDal.Setup(x => x.AddFlexWorker(It.IsAny<FlexworkerModel>()));

        // Act
        await _flexWorkerHandler.CreateFlexWorker(flexWorker);

        // Assert
        _mockFlexworkerDal.Verify(x => x.AddFlexWorker(It.IsAny<FlexworkerModel>()), Times.Once);
    }
    
    [TestMethod]
    public async Task CreateFlexWorker_ShouldThrowExceptionIfFlexWorkerAlreadyExists()
    {
        // Arrange
        var flexWorker = new FlexworkerModel
        {
            Id = 1, Name = "Flexworker1", Email = "email1@email.nl", Adress = "Adress1",
            DateOfBirth = new DateTime(1990, 10, 1), PhoneNumber = "0612345678", ProfilePictureUrl = "url1"
        };

        _mockFlexworkerDal.Setup(x => x.AddFlexWorker(It.IsAny<FlexworkerModel>()))
            .ThrowsAsync(new Exception("Flexworker already exists"));

        // Act
        var exception =
            await Assert.ThrowsExceptionAsync<Exception>(() => _flexWorkerHandler.CreateFlexWorker(flexWorker));
    }

    [TestMethod]
    public async Task DeleteFlexWorker_ShouldDeleteFlexWorker()
    {
        // Arrange
        var flexWorker = new FlexworkerModel
        {
            Id = 1, Name = "Flexworker1", Email = "email1@email.nl", Adress = "Adress1",
            DateOfBirth = new DateTime(1990, 10, 1), PhoneNumber = "0612345678", ProfilePictureUrl = "url1"
        };

        _mockFlexworkerDal.Setup(x => x.GetFlexWorkerById(It.IsAny<int>())).Returns(flexWorker);
        _mockFlexworkerDal.Setup(x => x.DeleteFlexWorker(It.IsAny<int>()));

        // Act
        await _flexWorkerHandler.DeleteFlexWorker(flexWorker.Id);

        // Assert
        _mockFlexworkerDal.Verify(x => x.DeleteFlexWorker(It.IsAny<int>()), Times.Once);
    }

    [TestMethod]
    public async Task DeleteFlexWorker_ShouldThrowExceptionIfFlexWorkerDoesNotExist()
    {
        // Arrange
        var flexWorker = new FlexworkerModel
        {
            Id = 1, Name = "Flexworker1", Email = "email1@email.nl", Adress = "Adress1",
            DateOfBirth = new DateTime(1990, 10, 1), PhoneNumber = "0612345678", ProfilePictureUrl = "url1"
        };

        _mockFlexworkerDal.Setup(x => x.GetFlexWorkerById(It.IsAny<int>())).Returns((FlexworkerModel)null);

        // Act
        var exception =
            await Assert.ThrowsExceptionAsync<Exception>(() => _flexWorkerHandler.DeleteFlexWorker(flexWorker.Id));

        // Assert
        Assert.AreEqual("Flexworker not found", exception.Message);
    }

    [TestMethod]
    public async Task UpdateFlexWorker_ShouldUpdateFlexWorker()
    {
        //Arrange
        var oldFlexWorker = new FlexworkerModel
        {
            Id = 1, Name = "OlD_Flexworker1", Email = "OLD_email1@email.nl", Adress = "OlD_Adress1",
            DateOfBirth = new DateTime(1990, 10, 1), PhoneNumber = "OlD_0612345678", ProfilePictureUrl = "OlD_url1"
        };
        var updatedFlexWorker = new FlexworkerModel
        {
            Id = 1, Name = "New_Flexworker1", Email = "New_email1@email.nl", Adress = "New_Adress1",
            DateOfBirth = new DateTime(2000, 11, 19), PhoneNumber = "New_0612345678", ProfilePictureUrl = "New_url1"
        };
        
        _mockFlexworkerDal.Setup(x => x.GetFlexWorkerById(It.IsAny<int>())).Returns(oldFlexWorker);
        _mockFlexworkerDal.Setup(x => x.UpdateFlexWorker(It.IsAny<FlexworkerModel>()));
        
        //Act
        
        await _flexWorkerHandler.UpdateFlexWorker(updatedFlexWorker);
        
        //Assert
        
        _mockFlexworkerDal.Verify(x => x.UpdateFlexWorker(It.Is<FlexworkerModel>(
            fw => fw.Id == updatedFlexWorker.Id &&
                  fw.Name == updatedFlexWorker.Name &&
                  fw.Email == updatedFlexWorker.Email &&
                  fw.Adress == updatedFlexWorker.Adress &&
                  fw.PhoneNumber == updatedFlexWorker.PhoneNumber &&
                  fw.ProfilePictureUrl == updatedFlexWorker.ProfilePictureUrl
        )), Times.Once);
        
        _mockFlexworkerDal.Verify(x => x.UpdateFlexWorker(It.Is<FlexworkerModel>(
            fw => fw.Id != oldFlexWorker.Id &&
                  fw.Name != oldFlexWorker.Name &&
                  fw.Email != oldFlexWorker.Email &&
                  fw.Adress != oldFlexWorker.Adress &&
                  fw.PhoneNumber != oldFlexWorker.PhoneNumber &&
                  fw.ProfilePictureUrl != oldFlexWorker.ProfilePictureUrl
        )), Times.Never);
    }
    [TestMethod]
    public async Task UpdateFlexWorker_ShouldNotUpdateIfFlexWorkerIsSame()
    {
        // Arrange
        var oldFlexWorker = new FlexworkerModel
        {
            Id = 1, Name = "Old_Flexworker", Email = "old@email.nl", Adress = "Old_Adress",
            DateOfBirth = new DateTime(1990, 10, 1), PhoneNumber = "0612345678", ProfilePictureUrl = "old_url"
        };

        var sameFlexWorker = new FlexworkerModel
        {
            Id = 1, Name = "Old_Flexworker", Email = "old@email.nl", Adress = "Old_Adress",
            DateOfBirth = new DateTime(1990, 10, 1), PhoneNumber = "0612345678", ProfilePictureUrl = "old_url"
        };

        _mockFlexworkerDal.Setup(x => x.GetFlexWorkerById(It.IsAny<int>())).Returns(oldFlexWorker);

        // Act
        await _flexWorkerHandler.UpdateFlexWorker(sameFlexWorker);
        
        _mockFlexworkerDal.Verify(x => x.UpdateFlexWorker(It.Is<FlexworkerModel>(
            fw => fw.Id == oldFlexWorker.Id &&
                  fw.Name != oldFlexWorker.Name ||
                  fw.Email != oldFlexWorker.Email || 
                  fw.Adress != oldFlexWorker.Adress ||
                  fw.PhoneNumber != oldFlexWorker.PhoneNumber ||
                  fw.ProfilePictureUrl != oldFlexWorker.ProfilePictureUrl
        )), Times.Never);
    }

    [TestMethod]
    public async Task UpdateFlexWorker_ShouldThrowExceptionIfFlexWorkerDoesNotExist()
    {
        {
            // Arrange
            var updatedFlexWorker = new FlexworkerModel
            {
                Id = 1, Name = "New_Flexworker1", Email = "New_email1@email.nl", Adress = "New_Adress1",
                DateOfBirth = new DateTime(1990, 10, 1), PhoneNumber = "New_0612345678", ProfilePictureUrl = "New_url1"
            };
            
            _mockFlexworkerDal.Setup(x => x.UpdateFlexWorker(It.IsAny<FlexworkerModel>())).ThrowsAsync(new Exception());

            // Act 
            async Task Act() => await _flexWorkerHandler.UpdateFlexWorker(updatedFlexWorker);
            
            // Assert
            await Assert.ThrowsExceptionAsync<Exception>(Act);
        }
    } 

}