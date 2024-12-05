using Interface.Dtos;
using Interface.Exceptions;
using Interface.Interface.Dal;
using Interface.Models;
using Logic.Handlers;
using Moq;

namespace Test.UnitTests.Handlers;

[TestClass]
public class SkillHandlerTests
{
    private Mock<ISkillDal> _mockSkillDal = null!;
    private SkillHandler _skillHandler = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockSkillDal = new Mock<ISkillDal>();
        _skillHandler = new SkillHandler(_mockSkillDal.Object);
    }
    
    //test for getskillfromid
    [TestMethod]
    public async Task GetSkillFromId_ShouldReturnSkill()
    {
        //Arrange
        SkillModel skill = new() { Id = 1, Name = "Skill1", CategoryId = 1};
        _mockSkillDal.Setup(x => x.GetSkill(It.IsAny<int>())).ReturnsAsync(skill);

        //Act
        var result = await _skillHandler.GetSkillFromId(1);

        //Assert
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual("Skill1", result.Name);
        Assert.AreEqual(1, result.CategoryId);
    }
    
    [TestMethod]
    public async Task GetSkillFromId_ShouldThrowExceptionIfSkillNotFound()
    {
        // Arrange
        _mockSkillDal.Setup(x => x.GetSkill(It.IsAny<int>()))
            .ThrowsAsync(new NotFoundException("Skill with id '1' not found."));
        
        // Act
        var exception = await Assert.ThrowsExceptionAsync<NotFoundException>(() => _skillHandler.GetSkillFromId(1));
        Assert.AreEqual("Skill with id '1' not found.", exception.Message);
    }
    
    //test for getskillsfromcategory
    
    [TestMethod]
    public async Task GetSkillsFromCategory_ShouldReturnListOfSkills()
    {
        //Arrange
        List<SkillModel> skills = new List<SkillModel>()
        {
            new() { Id = 1, Name = "Skill1", CategoryId = 1 },
            new() { Id = 2, Name = "Skill2", CategoryId = 1 },
            new() { Id = 3, Name = "Skill3", CategoryId = 1 },
        };

        int pageNumber = 1;
        int limit = 2;

        _mockSkillDal
            .Setup(x => x.GetSkillsFromCategory(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(skills.Skip((pageNumber - 1) * limit).Take(limit).ToList());

        // Act
        var result = await _skillHandler.GetSkillsFromCategory(pageNumber, limit, 1);

        // Assert
        Assert.AreEqual(2, result!.Count);
        Assert.AreEqual(1, result[0].Id);
        Assert.AreEqual(2, result[1].Id);
    }
    
    [TestMethod]
    public async Task GetSkillsFromCategory_ShouldThrowExceptionIfInvalidCategoryId()
    {
        // Arrange
        _mockSkillDal.Setup(x => x.GetSkillsFromCategory(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
            .ThrowsAsync(new Exception("Invalid category id"));
        
        // Act
        var exception = await Assert.ThrowsExceptionAsync<Exception>(() => _skillHandler.GetSkillsFromCategory(1, 2, 0));
        Assert.AreEqual("Invalid category id", exception.Message);
    }
    
    //test for createskill
    
    [TestMethod]
    public async Task CreateSkill_shouldCreateSkill()
    {
        //Arrange
        _mockSkillDal.Setup(x => x.CreateSkill(It.IsAny<string>(), It.IsAny<int>()));
        
        //Act
        await _skillHandler.CreateSkill("Skill1", 1);
        
        //Assert
        _mockSkillDal.Verify(x => x.CreateSkill("Skill1", 1), Times.Once);
    }
    
    [TestMethod]
    public async Task CreateSkill_ShouldThrowExceptionIfSkillAlreadyExists()
    {
        // Arrange
        _mockSkillDal.Setup(x => x.CreateSkill(It.IsAny<string>(), It.IsAny<int>()))
            .ThrowsAsync(new Exception("A skill with the name 'Skill1' already exists."));
        
        // Act
        var exception = await Assert.ThrowsExceptionAsync<Exception>(() => _skillHandler.CreateSkill("Skill1", 1));
        Assert.AreEqual("A skill with the name 'Skill1' already exists.", exception.Message);
    }
    
    //test for deleteskill
    
    [TestMethod]
    public async Task DeleteSkill_ShouldDeleteSkill()
    {
        // Arrange
        _mockSkillDal.Setup(x => x.DeleteSkill(It.IsAny<int>())).Returns(Task.CompletedTask);
        
        // Act
        await _skillHandler.DeleteSkill(1);
        
        // Assert
        _mockSkillDal.Verify(x => x.DeleteSkill(1), Times.Once);
    }
    
    [TestMethod]
    public async Task DeleteSkill_ShouldThrowExceptionIfSkillNotFound()
    {
        // Arrange
        _mockSkillDal.Setup(x => x.DeleteSkill(It.IsAny<int>()))
            .ThrowsAsync(new NotFoundException("Skill with id '1' not found."));
        
        // Act
        var exception = await Assert.ThrowsExceptionAsync<NotFoundException>(() => _skillHandler.DeleteSkill(1));
        Assert.AreEqual("Skill with id '1' not found.", exception.Message);
    }
    
    //test for updateskill
    
    [TestMethod]
    public async Task UpdateSkill_ShouldUpdateSkill()
    {
        // Arrange
        SkillModel skill = new() { Id = 1, Name = "Skill1", CategoryId = 1 };
        _mockSkillDal.Setup(x => x.UpdateSkill(It.IsAny<SkillModel>())).Returns(Task.CompletedTask);
        
        // Act
        await _skillHandler.UpdateSkill(new Skill { Id = 1, Name = "Skill1", CategoryId = 1 });
        
        // Assert
        _mockSkillDal.Verify(x => x.UpdateSkill(It.Is<SkillModel>(s =>
            s.Id == skill.Id &&
            s.Name == skill.Name &&
            s.CategoryId == skill.CategoryId)), Times.Once);
    }
    
    [TestMethod]
    public async Task UpdateSkill_ShouldThrowExceptionIfInvalidSkillId()
    {
        // Arrange
        _mockSkillDal.Setup(x => x.UpdateSkill(It.IsAny<SkillModel>()))
            .ThrowsAsync(new Exception("Invalid or no skill id provided"));
        
        // Act
        var exception = await Assert.ThrowsExceptionAsync<Exception>(() => _skillHandler.UpdateSkill(new Skill { Id = 0, Name = "Skill1", CategoryId = 1 }));
        Assert.AreEqual("Invalid or no skill id provided", exception.Message);
    }
    
    
}