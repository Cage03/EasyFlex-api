﻿using System;
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
        public void TestHardFilter1()
        {
            List<FlexworkerModel> flexworkers = flexworkerGenerator.FlexworkersForTest1();
            List<ResultModel> results = Algorithm.Execute(jobGenerator.Jobs[0], flexworkers);

            Assert.AreEqual(results[0].FlexworkerId, 1);
            Assert.AreEqual(results[0].Compatibility, 100);
            Assert.AreEqual(results.Count, 1);
        }

        [TestMethod]
        public void TestHardFilter2()
        {
            List<FlexworkerModel> flexworkers = flexworkerGenerator.FlexworkersForTest2();
            List<ResultModel> results = Algorithm.Execute(jobGenerator.Jobs[1], flexworkers);

            Assert.AreEqual(results[0].FlexworkerId, 3);
            Assert.AreEqual(results[0].Compatibility, 100);
            Assert.AreEqual(results.Count, 1);
        }

        [TestMethod]
        public void TestSoftFilter1()
        {
            List<FlexworkerModel> flexworkers = flexworkerGenerator.FlexworkersForTest1();
            List<ResultModel> results = Algorithm.Execute(jobGenerator.Jobs[2], flexworkers);

            Assert.AreEqual(results[0].Compatibility, 100);
            Assert.AreEqual(results[1].Compatibility, 0);
            Assert.AreEqual(results.Count, 2);
        }

        [TestMethod]
        public void TestSoftFilter2()
        {
            List<FlexworkerModel> flexworkers = flexworkerGenerator.FlexworkersForTest4();
            List<ResultModel> results = Algorithm.Execute(jobGenerator.Jobs[3], flexworkers);

            Assert.AreEqual(Math.Round(2.0 / 3.0 * 100.0), results[0].Compatibility);
            Assert.AreEqual(0, results[1].Compatibility);
            Assert.AreEqual(100, results[2].Compatibility);
            Assert.AreEqual(Math.Round(1.0 / 3.0 * 100.0), results[3].Compatibility);
        }


        [TestMethod]
        public void TestSoftAndHardFilter1()
        {
            List<FlexworkerModel> flexworkers = flexworkerGenerator.FlexworkersForTest5();
            List<ResultModel> results = Algorithm.Execute(jobGenerator.Jobs[4], flexworkers);

            Assert.AreEqual(results[0].Compatibility, 100);
            Assert.AreEqual(results[1].Compatibility, 50);
        }
    }
}
