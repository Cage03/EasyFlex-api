using System.Linq.Expressions;
using Interface.Dtos;
using Interface.Exceptions;
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
    public async Task CreateCategory_ShouldCreateCategory ()
    {
        //Arrange
        Category category = new() { Id = 1, Name = "Cat1"};
        _mockCategoryDal.Setup(x => x.CreateCategory(It.IsAny<CategoryModel>()));

        //Act
        await _categoryHandler.CreateCategory(category);

        //Assert
        _mockCategoryDal.Verify(x => x.CreateCategory(It.IsAny<CategoryModel>()), Times.Once());
    }

    [TestMethod]
    public async Task CreateCategory_ShouldThrowExceptionIfCategoryAlreadyExists()
    {
        // Arrange
        var category = new Category {Id = 1, Name = "Cat1" };

        _mockCategoryDal.Setup(x => x.CreateCategory(It.IsAny<CategoryModel>()))
            .ThrowsAsync(new Exception("Category already exists"));
        
        // Act
        var exception = await Assert.ThrowsExceptionAsync<Exception>(() => _categoryHandler.CreateCategory(category));
        Assert.AreEqual("Category already exists", exception.Message);
    }

    [TestMethod]
    public async Task GetCategories_ShouldReturnListOfCategories()
    {
        //Arrange
        List<Category> categories = new List<Category>()
        {
            new() { Id = 1, Name = "categorie1" },
            new() { Id = 2, Name = "categorie2" },
            new() { Id = 3, Name = "categorie3" },
        };

        int pageNumber = 1;
        int limit = 2;

        // ReSharper disable once UselessBinaryOperation
        var offset = (pageNumber - 1) * limit;

        _mockCategoryDal.Setup(x => x.GetCategories(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(categories.Skip(offset).Take(limit).Select(c => new CategoryModel { Id = c.Id, Name = c.Name }).ToList());
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
        var categories = new List<Category>(); // No jobs

        _mockCategoryDal.Setup(x => x.GetCategories(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(categories.Select(c => new CategoryModel { Id = c.Id, Name = c.Name }).ToList());

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
        Category category = new() { Id = 1, Name = "categorie1" };
        _mockCategoryDal.Setup(x => x.GetCategoryById(It.IsAny<int>())).ReturnsAsync(new CategoryModel { Id = category.Id, Name = category.Name });

        //Act
        var result = await _categoryHandler.GetCategoryById(1);

        //Assert
        Assert.AreEqual(category.Id, result.Id);
        Assert.AreEqual(category.Name, result.Name);
    }

    [TestMethod]
    public async Task UpdateCategory_ShouldReturnShouldBeSuccessful()
    {
        //Arrange
        Category oldCategory = new() { Id = 1, Name = "category1" };
        
        _mockCategoryDal.Setup(x => x.UpdateCategory(It.IsAny<CategoryModel>()));
        //Act
        
        await _categoryHandler.UpdateCategory(oldCategory);
        
        
        //Assert
       _mockCategoryDal.Verify(x => x.UpdateCategory(It.IsAny<CategoryModel>()), Times.Once);
        
    }
    [TestMethod]
    public async Task UpdateCategory_ShouldThrowAlreadyExists_WhenNameExistsWithDifferentId()
    {
        // Arrange
        var categoryToUpdate = new CategoryModel { Id = 1, Name = "categorie1" };
    
        _mockCategoryDal.Setup(x => x.UpdateCategory(It.IsAny<CategoryModel>())).ThrowsAsync(new Exception("alreadyExists"));
        // Act
        async Task Act() => await _categoryHandler.UpdateCategory(new Category { Id = categoryToUpdate.Id, Name = categoryToUpdate.Name });

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
        async Task Act() => await _categoryHandler.UpdateCategory(new Category { Id = categoryToUpdate.Id, Name = categoryToUpdate.Name });

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
        async Task Act() => await _categoryHandler.UpdateCategory(new Category { Id = categoryToUpdate.Id, Name = categoryToUpdate.Name });

        //Assert
        await Assert.ThrowsExceptionAsync<Exception>(Act);
    }
    
    [TestMethod]
    public async Task DeleteCategory_ShouldCallDalDeleteCategory()
    {
        // Arrange
        int categoryId = 1;
        _mockCategoryDal.Setup(x => x.DeleteCategory(categoryId)).Returns(Task.CompletedTask);

        // Act
        await _categoryHandler.DeleteCategory(categoryId);

        // Assert
        _mockCategoryDal.Verify(x => x.DeleteCategory(categoryId), Times.Once);
    }

    [TestMethod]
    public async Task DeleteCategory_ShouldNotCallDalIfInvalidId()
    {
        // Arrange
        int invalidCategoryId = -1;

        _mockCategoryDal.Setup(x => x.DeleteCategory(invalidCategoryId))
            .ThrowsAsync(new NotFoundException("Not found"));

        // Act & Assert
        await Assert.ThrowsExceptionAsync<NotFoundException>(async () => 
            await _categoryHandler.DeleteCategory(invalidCategoryId));
    }
}

