using DataAccess.Database;
using Interface.Dtos;
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
        public List<Job> Jobs;

        public JobGenerator(List<Preference> preferences)
        {
            Jobs = new List<Job>();

            var jobData = new List<(int Id, List<int> preferenceIndices)>
            {
                (0, new List<int> { 0 }), //nederlands
                (1, new List < int > { 0, 1 }), //nederlands en engels
                (2, new List<int> { 2 }), //nederlands
                (3, new List<int> { 2, 3 }), //nederlands en engels
                (4, new List<int> { 2, 3, 5 }), //nederlands, engels en havo
                (5, new List<int> { 0, 4 }), //nederlands en havo
                (6, new List<int> { 0, 3, 5 }), //nederlands, engels en havo
                (7, new List<int> {  0, 1, 5, 6}), //nederlands, engels, havo en heftruck
                (8, new List<int> { 6 }), //Heftruck
                (9, new List<int> { 3 }), //engels
                (10, new List<int> { 8 }), //Havo
                (11, new List<int> { 7 }), //nederlands
                (12, new List<int> { 3, 7 }), //nederlands en engels
                (13, new List<int> {7, 8}), //nederlands en havo
                (14, new List<int> { 1, 7 }), //engels vereist en nederlands voorkeur
                (15, new List<int> { 0, 1, 8 }), //engels nederlands vereist, havo voorkeur
                (16, new List<int> { 1, 7, 8 }), //engels vereist, nederlands havo voorkeur
                (17, new List<int> { 1, 8}), //engels vereist havo voorkeur
                
                
            };

            foreach(var j in jobData)
            {
                Job job = new Job()
                {
                    Id = j.Id,
                    Name = "name",
                    Address = "address",
                    Description = "desc",
                    MinHours = 4,
                    MaxHours = 8,
                    StartDate = new DateOnly(2022, 1, 1),
                    EndDate = new DateOnly(2022, 1, 1)
                };
                foreach (var preferenceIndex in j.preferenceIndices)
                {
                    job.Preferences.Add(preferences[(int)preferenceIndex]);
                }
                Jobs.Add(job);
            }
        }

        public List<Job> Jobs_Hardfilter_Sc_1()
        {
            List<Job> output = new List<Job>();
            output.Add(Jobs[0]);
            output.Add(Jobs[8]);
            return output;
        }
        
        public List<Job> Jobs_Hardfilter_Sc_2()
        {
            List<Job> output = new List<Job>();
            output.Add(Jobs[11]);
            output.Add(Jobs[2]);
            output.Add(Jobs[9]);
            output.Add(Jobs[10]);
            
            return output;
        }

        public List<Job> Jobs_Softfilter_Sc_1()
        {
            List<Job> output = new List<Job>();
            output.Add(Jobs[11]);
            output.Add(Jobs[10]);
            return output;

        }
        
        public List<Job> Jobs_Softfilter_Sc_2()
        {
            List<Job> output = new List<Job>();
            
            output.Add(Jobs[12]);
            output.Add(Jobs[11]);
            output.Add(Jobs[9]);
            output.Add(Jobs[13]);
            output.Add(Jobs[8]);
            
            return output;
        }

        public List<Job> Jobs_HardSoftFilter()
        {
            List<Job> output = new List<Job>();
            output.Add(Jobs[14]);
            output.Add(Jobs[15]);
            output.Add(Jobs[16]);
            output.Add(Jobs[17]);
            output.Add(Jobs[0]);
            output.Add(Jobs[11]);
            output.Add(Jobs[8]);
            
            return output;
        }
    }
}
