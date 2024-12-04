using Interface.Dtos;
using Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.AlgorithmTests
{
    public class SkillGenerator
    {
        public List<Skill> Skills = new List<Skill>();

        public SkillGenerator()
        {
            var skillData = new List<(int Id, string Name)>
            {
                (0, "Nederlandstalig"),
                (1, "Engelstalig"),
                (2, "Havodiploma"),
                (3, "Heftruck")
            };

            foreach (var data in skillData)
            {
                var skill = new Skill
                {
                    Id = data.Id,
                    Name = data.Name
                };

                Skills.Add(skill);
            }
        }
    }
}
