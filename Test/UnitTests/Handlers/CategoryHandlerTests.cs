using Interface.Interface.Dal;
using Interface.Models;
using Logic.Handlers;
using Moq;
namespace Test.UnitTests.Handlers;
[TestClass]
public class CategoryHandlerTests
{
    private Mock<ICategoryDal> _mockCategoryDal = null!;
    private CategoryHandler _categoryHandler = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockCategoryDal = new Mock<ICategoryDal>();
        _categoryHandler = new CategoryHandler(_mockCategoryDal.Object);
    }
    
    [TestMethod]
    public async Task CreateCategory_ShouldReturn1IfSuccessful ()
    {
        //Arrange
        CategoryModel job = new() { Id = 1, Name = "Job1"};
        _mockCategoryDal.Setup(x => x.CreateCategory(It.IsAny<CategoryModel>())).ReturnsAsync(1);

        //Act
        var result = await _categoryHandler.CreateCategory(job);

        //Assert
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public async Task CreateJob_ShouldReturn0IfUnsuccessful()
    {
        //Arrange
        CategoryModel job = new() { Id = 1, Name = "Job1"};
        _mockCategoryDal.Setup(x => x.CreateCategory(It.IsAny<CategoryModel>())).ReturnsAsync(0);

        //Act
        var result = await _categoryHandler.CreateCategory(job);

        //Assert
        Assert.AreEqual(0, result);
    }
    
}

