// using Interface.Models;
// using Logic.Classes;
//
// namespace Test.UnitTests.Classes
// {
//     [TestClass]
//     public class FlexWorkerTests
//     {
//         private FlexworkerModel _flexWorkerModel = null!;
//         private FlexWorker _flexWorker = null!;
//
//         [TestInitialize]
//         public void Setup()
//         {
//             // Initialize a FlexWorkerModel for testing
//             _flexWorkerModel = new FlexworkerModel
//             {
//                 Id = 1,
//                 Name = "John Doe",
//                 Address = "123 Street",
//                 DateOfBirth = new DateOnly(1990, 1, 1),
//                 Email = "john@example.com",
//                 PhoneNumber = "123-456-7890",
//                 ProfilePictureUrl = "https://example.com/profile.jpg",
//                 Skills = new List<SkillModel>
//                 {
//                     new SkillModel
//                     {
//                         Id = 1,
//                         CategoryId = 100,
//                         Name = "C#",
//                         Category = new CategoryModel { Id = 100, Name = "Programming" },
//                         Jobs = new List<JobModel>()
//                     },
//                     new SkillModel
//                     {
//                         Id = 2,
//                         CategoryId = 101,
//                         Name = "SQL",
//                         Category = new CategoryModel { Id = 101, Name = "Database" },
//                         Jobs = new List<JobModel>()
//                     }
//                 }
//             };
//
//             _flexWorker = new FlexWorker(_flexWorkerModel);
//         }
//
//         [TestMethod]
//         public void Constructor_InitializesPropertiesCorrectly()
//         {
//             // Assert the properties were set correctly by the constructor
//             Assert.AreEqual(_flexWorkerModel.Id, _flexWorker.Id);
//             Assert.AreEqual(_flexWorkerModel.Name, _flexWorker.Name);
//             Assert.AreEqual(_flexWorkerModel.Address, _flexWorker.Address);
//             Assert.AreEqual(_flexWorkerModel.DateOfBirth, _flexWorker.DateOfBirth);
//             Assert.AreEqual(_flexWorkerModel.Email, _flexWorker.Email);
//             Assert.AreEqual(_flexWorkerModel.PhoneNumber, _flexWorker.PhoneNumber);
//             Assert.AreEqual(_flexWorkerModel.ProfilePictureUrl, _flexWorker.ProfilePictureUrl);
//             Assert.AreEqual(_flexWorkerModel.Skills.Count, _flexWorker.Skills.Count);
//
//             // Assert Skills are mapped correctly
//             Assert.IsTrue(_flexWorkerModel.Skills.First().Name == _flexWorker.Skills.First().Name);
//             Assert.IsTrue(_flexWorkerModel.Skills.Last().Category.Name == _flexWorker.Skills.Last().Category.Name);
//         }
//
//         [TestMethod]
//         public void ToModel_ReturnsCorrectModel()
//         {
//             // Act
//             var model = _flexWorker.ToModel();
//
//             // Assert the properties are mapped correctly back to the model
//             Assert.AreEqual(_flexWorker.Id, model.Id);
//             Assert.AreEqual(_flexWorker.Name, model.Name);
//             Assert.AreEqual(_flexWorker.Address, model.Address);
//             Assert.AreEqual(_flexWorker.DateOfBirth, model.DateOfBirth);
//             Assert.AreEqual(_flexWorker.Email, model.Email);
//             Assert.AreEqual(_flexWorker.PhoneNumber, model.PhoneNumber);
//             Assert.AreEqual(_flexWorker.ProfilePictureUrl, model.ProfilePictureUrl);
//             Assert.AreEqual(_flexWorker.Skills.Count, model.Skills.Count);
//
//             // Assert Skills are mapped correctly back to SkillModel
//             Assert.AreEqual(_flexWorker.Skills.First().Name, model.Skills.First().Name);
//             Assert.AreEqual(_flexWorker.Skills.Last().Category.Name, model.Skills.Last().Category.Name);
//         }
//
//         [TestMethod]
//         public void Constructor_WithNullPhoneNumber_InitializesCorrectly()
//         {
//             // Arrange
//             _flexWorkerModel.PhoneNumber = null!;
//
//             // Act
//             _flexWorker = new FlexWorker(_flexWorkerModel);
//
//             // Assert
//             Assert.IsNull(_flexWorker.PhoneNumber);
//         }
//
//         [TestMethod]
//         public void ToModel_ReturnsNullPhoneNumber_WhenNotSet()
//         {
//             // Arrange
//             _flexWorker.PhoneNumber = null;
//
//             // Act
//             var model = _flexWorker.ToModel();
//
//             // Assert
//             Assert.IsNull(model.PhoneNumber);
//         }
//
//         [TestMethod]
//         public void Constructor_WithEmptySkills_InitializesCorrectly()
//         {
//             // Arrange
//             _flexWorkerModel.Skills = new List<SkillModel>();
//
//             // Act
//             _flexWorker = new FlexWorker(_flexWorkerModel);
//
//             // Assert
//             Assert.IsNotNull(_flexWorker.Skills);
//             Assert.AreEqual(0, _flexWorker.Skills.Count);
//         }
//
//         [TestMethod]
//         public void ToModel_ReturnsEmptySkills_WhenNotSet()
//         {
//             // Arrange
//             _flexWorker.Skills = new List<Skill>();
//
//             // Act
//             var model = _flexWorker.ToModel();
//
//             // Assert
//             Assert.IsNotNull(model.Skills);
//             Assert.AreEqual(0, model.Skills.Count);
//         }
//
//         [TestMethod]
//         public void Constructor_SetsCategoryCorrectlyForSkills()
//         {
//             // Assert that the Category for each Skill is initialized correctly
//             var firstSkill = _flexWorker.Skills.First();
//             Assert.AreEqual(_flexWorkerModel.Skills.First().Category.Name, firstSkill.Category.Name);
//             Assert.AreEqual(_flexWorkerModel.Skills.First().CategoryId, firstSkill.CategoryId);
//         }
//     }
// }
