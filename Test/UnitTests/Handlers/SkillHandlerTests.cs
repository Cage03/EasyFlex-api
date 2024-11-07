using Interface.Dto;
using Interface.Interface.Dal;
using Interface.Models;
using Logic.Handlers;
using Moq;

namespace Test.UnitTests.Handlers
{
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

        [TestMethod]
        public async Task GetSkillsFromIds_ShouldReturnSkills()
        {
            // Arrange
            var skills = new List<SkillModel>
            {
                new() { Id = 1, Name = "Skill1", CategoryId = 1 },
                new() { Id = 2, Name = "Skill2", CategoryId = 1 }
            };
            var skillIds = new List<int> { 1, 2 };
            _mockSkillDal
                .Setup(x => x.GetSkills(skillIds))
                .ReturnsAsync(skills);

            // Act
            var result = await _skillHandler.GetSkillsFromIds(skillIds);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Skill1", result[0].Name);
            Assert.AreEqual("Skill2", result[1].Name);
        }

        [TestMethod]
        public async Task GetSkillsFromIds_ShouldThrowExceptionIfNotAllSkillsFound()
        {
            // Arrange
            var skills = new List<SkillModel>
            {
                new() { Id = 1, Name = "Skill1", CategoryId = 1 }
            };
            var skillIds = new List<int> { 1, 2 };
            _mockSkillDal
                .Setup(x => x.GetSkills(skillIds))
                .ReturnsAsync(skills);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () =>
                await _skillHandler.GetSkillsFromIds(skillIds));
        }

        [TestMethod]
        public async Task GetSkillsFromCategory_ShouldReturnPaginatedSkills()
        {
            // Arrange
            var skills = new List<SkillModel>
            {
                new() { Id = 1, Name = "Skill1", CategoryId = 1 },
                new() { Id = 2, Name = "Skill2", CategoryId = 1 },
                new() { Id = 3, Name = "Skill3", CategoryId = 1 }
            };

            int pageNumber = 1;
            int limit = 2;
            int categoryId = 1;
            
            // ReSharper disable once UselessBinaryOperation
            var offset = (pageNumber - 1) * limit;
            
            _mockSkillDal
                .Setup(x => x.GetSkillsFromCategory(limit, pageNumber, categoryId))
                .ReturnsAsync(skills.Skip(offset).Take(limit).ToList());

            // Act
            var result = await _skillHandler.GetSkillsFromCategory(limit, pageNumber, categoryId);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Skill1", result[0].Name);
            Assert.AreEqual("Skill2", result[1].Name);
        }

        [TestMethod]
        public async Task GetSkillsFromCategory_ShouldThrowExceptionIfInvalidCategoryId()
        {
            // Arrange
            int invalidCategoryId = -1;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () =>
                await _skillHandler.GetSkillsFromCategory(10, 0, invalidCategoryId));
        }

        [TestMethod]
        public async Task CreateSkill_ShouldCallDalCreateSkillWithCorrectParameters()
        {
            // Arrange
            string skillName = "UniqueSkill";
            int categoryId = 1;
            _mockSkillDal.Setup(x => x.CreateSkill(skillName, categoryId)).Returns(Task.CompletedTask);

            // Act
            await _skillHandler.CreateSkill(skillName, categoryId);

            // Assert
            _mockSkillDal.Verify(x => x.CreateSkill(skillName, categoryId), Times.Once);
        }

        [TestMethod]
        public async Task CreateSkill_ShouldThrowExceptionIfSkillAlreadyExists()
        {
            // Arrange
            string duplicateSkillName = "DuplicateSkill";
            int categoryId = 1;
            _mockSkillDal
                .Setup(x => x.CreateSkill(duplicateSkillName, categoryId))
                .ThrowsAsync(new Exception("A skill with this name already exists."));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => 
                await _skillHandler.CreateSkill(duplicateSkillName, categoryId));
        }
        
        [TestMethod]
        public async Task DeleteSkill_ShouldCallDalDeleteSkillWithCorrectId()
        {
            // Arrange
            int validSkillId = 1;
            _mockSkillDal.Setup(x => x.DeleteSkill(validSkillId)).Returns(Task.CompletedTask);

            // Act
            await _skillHandler.DeleteSkill(validSkillId);

            // Assert
            _mockSkillDal.Verify(x => x.DeleteSkill(validSkillId), Times.Once);
        }

        [TestMethod]
        public async Task DeleteSkill_ShouldThrowExceptionIfIdIsInvalid()
        {
            // Arrange
            int invalidSkillId = -1;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<IndexOutOfRangeException>(async () =>
                await _skillHandler.DeleteSkill(invalidSkillId));
        }
        
        [TestMethod]
        public async Task UpdateSkill_ShouldCallDalUpdateSkillWithCorrectSkill()
        {
            // Arrange
            var validSkillDto = new SkillDto { Id = 1, Name = "UpdatedSkill", CategoryId = 1 };
            _mockSkillDal.Setup(x => x.UpdateSkill(validSkillDto)).Returns(Task.CompletedTask);

            // Act
            await _skillHandler.UpdateSkill(validSkillDto);

            // Assert
            _mockSkillDal.Verify(x => x.UpdateSkill(validSkillDto), Times.Once);
        }

        [TestMethod]
        public async Task UpdateSkill_ShouldThrowExceptionIfSkillIdIsInvalid()
        {
            // Arrange
            var invalidSkillDto = new SkillDto { Id = 0, Name = "InvalidSkill", CategoryId = 1 };

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () =>
                await _skillHandler.UpdateSkill(invalidSkillDto));
        }
    }
}
