using Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.AlgorithmTests
{
    public class PreferenceGenerator
    {
        public List<PreferenceModel> Preferences = new List<PreferenceModel>();

        public PreferenceGenerator()
        {
            var preferenceData = new List<(int Id, string Name, int SkillId, bool IsRequired, int Weight)>
            {
                (0, "Nederlands", 0, true, 100),
                (1, "Engels", 1, true, 100),
                (2, "Nederlands", 0, false, 100),
                (3, "Engels", 1, false, 50),
                (4, "Engels", 1, false, 100),
                (5, "Havodiploma", 2, false, 75)
            };

            foreach (var data in preferenceData)
            {
                var preference = new PreferenceModel
                {
                    Id = data.Id,
                    SkillId = data.SkillId,
                    IsRequired = data.IsRequired,
                    Weight = data.Weight
                };

                Preferences.Add(preference);
            }
        }
    }
}
