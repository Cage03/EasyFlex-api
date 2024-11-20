using DataAccess.Models;
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
        CategoryModel? job = new() { Id = 1, Name = "Job1"};
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
        CategoryModel? job = new() { Id = 1, Name = "Job1"};
        _mockCategoryDal.Setup(x => x.CreateCategory(It.IsAny<CategoryModel>())).ReturnsAsync(0);

        //Act
        var result = await _categoryHandler.CreateCategory(job);

        //Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public async Task GetCategories_ShouldReturnListOfCategories()
    {
        //Arrange
        List<CategoryModel>? categories = new List<CategoryModel>()
        {
            new() { Id = 1, Name = "categorie1" },
            new() { Id = 2, Name = "categorie2" },
            new() { Id = 3, Name = "categorie3" },
        };
        
        int pageNumber = 1;
        int limit = 2;
        
        // ReSharper disable once UselessBinaryOperation
        var offset = (pageNumber - 1) * limit;
        
        _mockCategoryDal.Setup(x =>x.GetCategories(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(categories.Skip(offset).Take(limit).ToList());
        //Act
        var result = await _categoryHandler.GetCategories(pageNumber, limit);
        // Assert
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(1, result[0].Id); 
        Assert.AreEqual(2, result[1].Id); 
        
    }
    
    [TestMethod]
    public async Task GetCategories_ShouldReturnEmptyListOfCategories()
    {
        // Arrange
        var categories = new List<CategoryModel>(); // No jobs

        _mockCategoryDal.Setup(x => x.GetCategories(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(categories);

        int pageNumber = 1;
        int limit = 2;

        // Act
        var result = await _categoryHandler.GetCategories(pageNumber, limit);

        // Assert
        Assert.AreEqual(0, result.Count);
    }
    
    [TestMethod]
    public async Task GetCategories_ShouldReturnCategorieb()
    {
        //Arrange
        CategoryModel category = new() { Id = 1, Name = "categorie1" };
        _mockCategoryDal.Setup(x => x.GetCategoryById(It.IsAny<int>())).ReturnsAsync(category);

        //Act
        var result = await _categoryHandler.GetCategoryById(1);

        //Assert
        Assert.AreEqual(category, result);
    }

    [TestMethod]
    public async Task GetCategorieByID_ShouldReturnNullIfCategorieDoesNotExist()
    {
        //Arrange
        _mockCategoryDal.Setup(x => x.GetCategoryById(It.IsAny<int>())).ReturnsAsync((CategoryModel?)null);

        //Act
        var result = await _categoryHandler.GetCategoryById(1);

        //Assert
        Assert.IsNull(result);
    }


    [TestMethod]
    public async Task UpdateCategory_ShouldReturnShouldBeSuccessful()
    {
        //Arrange
        CategoryModel oldCategory = new() { Id = 1, Name = "categorie1" };
        
        _mockCategoryDal.Setup(x => x.UpdateCategory(It.IsAny<CategoryModel>())).Returns(Task.CompletedTask);
        //Act
        
        await _categoryHandler.UpdateCategory(oldCategory);
        
        
        //Assert
       _mockCategoryDal.Verify(x => x.UpdateCategory(oldCategory), Times.Once);
        
    }
    [TestMethod]
    public async Task UpdateCategory_ShouldThrowAlreadyExists_WhenNameExistsWithDifferentId()
    {
        // Arrange
        var categoryToUpdate = new CategoryModel { Id = 1, Name = "categorie1" };
    
        _mockCategoryDal.Setup(x => x.UpdateCategory(It.IsAny<CategoryModel>())).ThrowsAsync(new Exception("alreadyExists"));
        // Act
        async Task Act() => await _categoryHandler.UpdateCategory(categoryToUpdate);

        //Assert
        await Assert.ThrowsExceptionAsync<Exception>(Act);
    }

    [TestMethod]
    public async Task UpdateCategory_ShouldThrowIsSameName_WhenNameIsSame()
    {
        // Arrange
        var categoryToUpdate = new CategoryModel { Id = 1, Name = "categorie1" };

        _mockCategoryDal.Setup(x => x.UpdateCategory(It.IsAny<CategoryModel>())).ThrowsAsync(new Exception("isSameName"));
        // Act
        async Task Act() => await _categoryHandler.UpdateCategory(categoryToUpdate);

        //Assert
        await Assert.ThrowsExceptionAsync<Exception>(Act);
    }

    [TestMethod]
    public async Task UpdateCategory_ShouldThrowDoesNotExist_WhenCategoryDoesNotExist()
    {
        // Arrange
        var categoryToUpdate = new CategoryModel { Id = 1, Name = "categorie1" };

        _mockCategoryDal.Setup(x => x.UpdateCategory(It.IsAny<CategoryModel>())).ThrowsAsync(new Exception("doesNotExist"));
        // Act
        async Task Act() => await _categoryHandler.UpdateCategory(categoryToUpdate);

        //Assert
        await Assert.ThrowsExceptionAsync<Exception>(Act);
    }
}

