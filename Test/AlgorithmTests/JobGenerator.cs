using DataAccess.Database;
using Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.AlgorithmTests
{
    public class JobGenerator
    {
        public List<JobModel> Jobs;

        public JobGenerator(List<PreferenceModel> preferences)
        {
            Jobs = new List<JobModel>();

            var jobData = new List<(int Id, List<int> preferenceIndices)>
            {
                (0, new List<int> { 0 }),
                (1, new List < int > { 0, 1 }),
                (2, new List<int> { 2 }),
                (3, new List<int> { 2, 3 }),
                (4, new List<int> { 2, 3, 5 }),
                (5, new List<int> { 0, 4 }),
                (6, new List<int> { 1, 4 }),
                (7, new List<int> {  0, 1, 5, 6}),     
            };

            foreach(var job in jobData)
            {
                JobModel jobModel = new JobModel()
                {
                    Id = job.Id,
                    Name = "name",
                    Description = "desc",
                    MinHours = 4,
                    MaxHours = 8,
                    StartDate = new DateOnly(2022, 1, 1),
                    EndDate = new DateOnly(2022, 1, 1)
                };
                foreach (var preferenceIndex in job.preferenceIndices)
                {
                    jobModel.Preferences.Add(preferences[preferenceIndex]);
                }
                Jobs.Add(jobModel);
            }
        }
    }
}
