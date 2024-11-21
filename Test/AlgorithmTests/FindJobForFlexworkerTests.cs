using Interface.Models;
using Logic;

namespace Test.AlgorithmTests
{
    [TestClass]
    public class FindJobForFlexworkerTests
    {
        private SkillGenerator skillGenerator;
        private PreferenceGenerator preferenceGenerator;
        private JobGenerator jobGenerator;
        private FlexworkerGenerator flexworkerGenerator;


        [TestInitialize]
        public void Setup()
        {
            skillGenerator = new SkillGenerator();
            preferenceGenerator = new PreferenceGenerator();
            jobGenerator = new JobGenerator(preferenceGenerator.Preferences);
            flexworkerGenerator = new FlexworkerGenerator(skillGenerator.Skills);

        }

        [TestMethod]
        public void Test_Sc_1_HardFilter()
        {
            var flexworker = flexworkerGenerator.Flexworkers_Sc_1_3()[0];
            
            List<JobResultModel> results = Algorithm.FindJobsForFlexworker(flexworker, jobGenerator.Jobs_Sc_1());

            Assert.AreEqual(0, results[0].JobId);
            Assert.AreEqual(1, results.Count);

            Assert.AreEqual(100, results[0].Compatibility);
        }
    }
}
