using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.Models;
using Logic;
using Moq;

namespace Test.AlgorithmTests
{
    [TestClass]
    public class AlgorithmUnitTests
    {
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private SkillGenerator skillGenerator;
        private PreferenceGenerator preferenceGenerator;
        private JobGenerator jobGenerator;
        private FlexworkerGenerator flexworkerGenerator;
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.


        [TestInitialize]
        public void Setup()
        {
            skillGenerator = new SkillGenerator();
            preferenceGenerator = new PreferenceGenerator();
            jobGenerator = new JobGenerator(preferenceGenerator.Preferences);
            flexworkerGenerator = new FlexworkerGenerator(skillGenerator.Skills);

        }

        [TestMethod]
        public void Test_Sc_1()
        {
            List<FlexworkerModel> flexworkers = flexworkerGenerator.Flexworkers_Sc_1_3();
            List<ResultModel> results = Algorithm.Execute(jobGenerator.Jobs[0], flexworkers);

            Assert.AreEqual(0, results[0].FlexworkerId);
            Assert.AreEqual(100, results[0].Compatibility);
            Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void Test_Sc_2()
        {
            List<FlexworkerModel> flexworkers = flexworkerGenerator.Flexworkers_Sc_2_4();
            List<ResultModel> results = Algorithm.Execute(jobGenerator.Jobs[1], flexworkers);

            Assert.AreEqual(2, results[0].FlexworkerId);
            Assert.AreEqual(100, results[0].Compatibility);
            Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void Test_Sc_3()
        {
            List<FlexworkerModel> flexworkers = flexworkerGenerator.Flexworkers_Sc_1_3();
            List<ResultModel> results = Algorithm.Execute(jobGenerator.Jobs[2], flexworkers);

            Assert.AreEqual(100, results[0].Compatibility);
            Assert.AreEqual(0, results[1].Compatibility);
            Assert.AreEqual(2, results.Count);
        }

        [TestMethod]
        public void Test_Sc_4()
        {
            List<FlexworkerModel> flexworkers = flexworkerGenerator.Flexworkers_Sc_2_4();
            List<ResultModel> results = Algorithm.Execute(jobGenerator.Jobs[3], flexworkers);

            Assert.AreEqual(Math.Round(2.0 / 3.0 * 100.0), results[0].Compatibility);
            Assert.AreEqual(0, results[1].Compatibility);
            Assert.AreEqual(100, results[2].Compatibility);
            Assert.AreEqual(Math.Round(1.0 / 3.0 * 100.0), results[3].Compatibility);
        }

        [TestMethod]
        public void Test_Sc_5()
        {
            List<FlexworkerModel> flexworkers = flexworkerGenerator.Flexworkers_Sc_5();
            List<ResultModel> results = Algorithm.Execute(jobGenerator.Jobs[7], flexworkers);

            Assert.AreEqual(100, results[0].Compatibility);
            Assert.AreEqual(Math.Round(175.0 / 225.0 * 100.0), results[1].Compatibility);
            Assert.AreEqual(Math.Round(150.0 / 225.0 * 100.0), results[2].Compatibility);
            Assert.AreEqual(Math.Round(125.0 / 225.0 * 100.0), results[3].Compatibility);
            Assert.AreEqual(Math.Round(75.0 / 225.0 * 100.0), results[4].Compatibility);
            Assert.AreEqual(Math.Round(50.0 / 225.0 * 100.0), results[5].Compatibility);
        }

        [TestMethod]
        public void Test_Sc_6()
        {
            List<FlexworkerModel> flexworkers = flexworkerGenerator.Flexworkers_Sc_6();
            List<ResultModel> results = Algorithm.Execute(jobGenerator.Jobs[4], flexworkers);

            Assert.AreEqual(100, results[0].Compatibility);
            Assert.AreEqual(50, results[1].Compatibility);
        }

        [TestMethod]
        public void Test_Sc_7()
        {
            List<FlexworkerModel> flexworkers = flexworkerGenerator.Flexworkers_Sc_7();
            List<ResultModel> results = Algorithm.Execute(jobGenerator.Jobs[6], flexworkers);

            Assert.AreEqual(100, results[3].Compatibility);
            Assert.AreEqual(Math.Round(175.0 / 225 * 100), results[2].Compatibility);
            Assert.AreEqual(Math.Round(150.0 / 225 * 100), results[1].Compatibility);
            Assert.AreEqual(Math.Round(100.0 / 225 * 100), results[0].Compatibility);
        }
    }
}
