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
                (1, "Nederlands", 1, true, 100),
                (2, "Engels", 2, true, 100),
                (3, "Nederlands", 1, false, 100),
                (4, "Engels", 2, false, 50),
                (5, "Engels", 2, false, 100)
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
