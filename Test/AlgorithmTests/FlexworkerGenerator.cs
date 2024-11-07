using Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.AlgorithmTests
{
    public class FlexworkerGenerator
    {
        private List<FlexworkerModel> flexworkers;

        public FlexworkerGenerator(List<SkillModel> skills)
        {
            flexworkers = new List<FlexworkerModel>();

            var flexworkerData = new List<(int Id, string Name, List<int> SkillIndices)>
            {
                (0, "Nederlandstalig", new List<int> { 0 }),
                (1, "Geen skills",new List<int>()),
                (2, "Nederlands en engels",new List < int > { 0, 1 }),
                (3, "Engels",new List < int > { 1 }),
                (4, "Nederlands en Havodiploma",new List < int > { 0, 2}),
                (5, "Nederlands, Havodiploma, engels",new List < int > { 0, 1, 2}),
            };

            foreach (var data in flexworkerData)
            {
                var flexworker = new FlexworkerModel
                {
                    Id = data.Id,
                    Name = data.Name,
                    Email = "email",
                    PhoneNumber = "0612345678",
                    DateOfBirth = new DateOnly(1990, 10, 1),
                    Address = "Adress",
                    ProfilePictureUrl = "url",
                };

                foreach (var skillIndex in data.SkillIndices)
                {
                    flexworker.Skills.Add(skills[skillIndex]);
                }

                flexworkers.Add(flexworker);
            }
        }

        public List<FlexworkerModel> FlexworkersForTest1()
        {
            List<FlexworkerModel> output = new List<FlexworkerModel>();
            output.Add(flexworkers[0]);
            output.Add(flexworkers[1]);

            return output;
        }

        public List<FlexworkerModel> FlexworkersForTest2()
        {
            List<FlexworkerModel> output = new List<FlexworkerModel>();
            output.Add(flexworkers[0]);
            output.Add(flexworkers[1]);
            output.Add(flexworkers[2]);
            output.Add(flexworkers[3]);

            return output;
        }

        public List<FlexworkerModel> FlexworkersForTest4()
        {
            List<FlexworkerModel> output = new List<FlexworkerModel>();
            //Nederlandstalig
            output.Add(flexworkers[0]);
            //Geen skills
            output.Add(flexworkers[1]);
            //Nederlands en engels
            output.Add(flexworkers[2]);
            //Engels
            output.Add(flexworkers[3]);

            return output;
        }

        public List<FlexworkerModel> FlexworkersForTest5()
        {
            List<FlexworkerModel> output = new List<FlexworkerModel>();
            //Nederlands en Engels
            output.Add(flexworkers[2]);
            //Nederlands
            output.Add(flexworkers[0]);
            //Geen skills
            output.Add(flexworkers[1]);

            return output;
        }

        public List<FlexworkerModel> FlexworkersForTest6()
        {
            List<FlexworkerModel> output = new List<FlexworkerModel>();
            output.Add(flexworkers[0]);
            output.Add(flexworkers[2]);
            output.Add(flexworkers[3]);
            output.Add(flexworkers[4]);
            output.Add(flexworkers[5]);
            return output;
        }
    }
}
