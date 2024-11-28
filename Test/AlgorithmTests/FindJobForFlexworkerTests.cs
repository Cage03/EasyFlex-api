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
            
            List<JobResultModel> results = Algorithm.FindJobsForFlexworker(flexworker, jobGenerator.Jobs_Hardfilter_Sc_1());

            Assert.AreEqual(0, results[0].JobId);
            Assert.AreEqual(1, results.Count);

            Assert.AreEqual(100, results[0].Compatibility);
        }

        [TestMethod]
        public void Test_Sc_2_HardFilter()
        {
            var flexworker = flexworkerGenerator.FlexworkerModel_Sc_2();
            
            List<JobResultModel> results = Algorithm.FindJobsForFlexworker(flexworker, jobGenerator.Jobs_Hardfilter_Sc_2());
            
            Assert.AreEqual(3, results.Count);
            Assert.AreEqual(100, results[0].Compatibility);
            Assert.AreEqual(100, results[1].Compatibility);
            Assert.AreEqual(100, results[2].Compatibility);
        }

        [TestMethod]
        public void Test_Sc_3_SoftFilter()
        {
            var flexworker = flexworkerGenerator.Flexworker_Sc_1();
            
            List<JobResultModel> results = Algorithm.FindJobsForFlexworker(flexworker, jobGenerator.Jobs_Softfilter_Sc_1());
            
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(100, results[0].Compatibility);
        }
        
        [TestMethod]
        public void Test_Sc_4_SoftFilter()
        {
            var flexworker = flexworkerGenerator.FlexworkerModel_Sc_2();
            
            List<JobResultModel> results = Algorithm.FindJobsForFlexworker(flexworker, jobGenerator.Jobs_Softfilter_Sc_2());
            
            Assert.AreEqual(4, results.Count);
            Assert.AreEqual(100, results[0].Compatibility);
            Assert.AreEqual(100, results[1].Compatibility);
            Assert.AreEqual(100, results[2].Compatibility);
            Assert.AreEqual(50, results[3].Compatibility);
            
        }

        [TestMethod]
        public void Test_Sc_5_HardSoftFilter()
        {
            var flexworker = flexworkerGenerator.FlexworkerModel_Sc_2();

            List<JobResultModel> results =
                Algorithm.FindJobsForFlexworker(flexworker, jobGenerator.Jobs_HardSoftFilter());
            
           Assert.AreEqual(6, results.Count);
           Assert.AreEqual(100, results[0].Compatibility);
           Assert.AreEqual(80, results[1].Compatibility);
           Assert.AreEqual(75, results[2].Compatibility);
           Assert.AreEqual(67, results[3].Compatibility);
           Assert.AreEqual(100, results[4].Compatibility);
           Assert.AreEqual(100, results[5].Compatibility);
        }
    }
}
