using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.Dtos;
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
        public void Test_Sc_1_HardFilter()
        {
            List<Flexworker> flexworkers = flexworkerGenerator.Flexworkers_Sc_1_3();
            List<FlexworkerResult> results = Algorithm.FindFlexworkersForJob(jobGenerator.Jobs[0], flexworkers);

            Assert.AreEqual(0, results[0].Id);
            Assert.AreEqual(1, results.Count);

            Assert.AreEqual(100, results[0].Compatibility);
        }

        [TestMethod]
        public void Test_Sc_2_HardFilter()
        {
            List<Flexworker> flexworkers = flexworkerGenerator.Flexworkers_Sc_2_4();
            List<FlexworkerResult> results = Algorithm.FindFlexworkersForJob(jobGenerator.Jobs[1], flexworkers);

            Assert.AreEqual(2, results[0].Id);
            Assert.AreEqual(1, results.Count);

            Assert.AreEqual(100, results[0].Compatibility);
        }

        [TestMethod]
        public void Test_Sc_3()
        {
            List<Flexworker> flexworkers = flexworkerGenerator.Flexworkers_Sc_1_3();
            List<FlexworkerResult> results = Algorithm.FindFlexworkersForJob(jobGenerator.Jobs[2], flexworkers);


            Assert.AreEqual(2, results.Count);

            Assert.AreEqual(100, results[0].Compatibility);
            Assert.AreEqual(0, results[1].Compatibility);
        }

        [TestMethod]
        public void Test_Sc_4()
        {
            List<Flexworker> flexworkers = flexworkerGenerator.Flexworkers_Sc_2_4();
            List<FlexworkerResult> results = Algorithm.FindFlexworkersForJob(jobGenerator.Jobs[3], flexworkers);

            Assert.AreEqual(100, results[0].Compatibility);
            Assert.AreEqual(67, results[1].Compatibility);
            Assert.AreEqual(33, results[2].Compatibility);
            Assert.AreEqual(0, results[3].Compatibility);
        }

        [TestMethod]
        public void Test_Sc_5()
        {
            List<Flexworker> flexworkers = flexworkerGenerator.Flexworkers_Sc_5();
            List<FlexworkerResult> results = Algorithm.FindFlexworkersForJob(jobGenerator.Jobs[4], flexworkers);

            Assert.AreEqual(100, results[0].Compatibility);
            Assert.AreEqual(78, results[1].Compatibility);
            Assert.AreEqual(67, results[2].Compatibility);
            Assert.AreEqual(56, results[3].Compatibility);
            Assert.AreEqual(33, results[4].Compatibility);
            Assert.AreEqual(22, results[5].Compatibility);
        }

        [TestMethod]
        public void Test_Sc_6()
        {
            List<Flexworker> flexworkers = flexworkerGenerator.Flexworkers_Sc_6();
            List<FlexworkerResult> results = Algorithm.FindFlexworkersForJob(jobGenerator.Jobs[5], flexworkers);

            Assert.AreEqual(2, results.Count);

            Assert.AreEqual(100, results[0].Compatibility);
            Assert.AreEqual(50, results[1].Compatibility);
        }

        [TestMethod]
        public void Test_Sc_7()
        {
            List<Flexworker> flexworkers = flexworkerGenerator.Flexworkers_Sc_7();
            List<FlexworkerResult> results = Algorithm.FindFlexworkersForJob(jobGenerator.Jobs[6], flexworkers);

            Assert.AreEqual(4, results.Count);


            Assert.AreEqual(100, results[0].Compatibility);
            Assert.AreEqual(78, results[1].Compatibility);
            Assert.AreEqual(67, results[2].Compatibility);
            Assert.AreEqual(44, results[3].Compatibility);
        }

        [TestMethod]
        public void Test_Sc_8()
        {
            List<Flexworker> flexworkers = flexworkerGenerator.Flexworkers_Sc_8();
            List<FlexworkerResult> results = Algorithm.FindFlexworkersForJob(jobGenerator.Jobs[7], flexworkers);

            Assert.AreEqual(100, results[0].Compatibility);
            Assert.AreEqual(85, results[1].Compatibility);
            Assert.AreEqual(62, results[2].Compatibility);
        }
    }
}
