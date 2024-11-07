using Interface.Models;
using Logic.Classes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public static class Algorithm
    {
        public static List<ResultModel> Execute(JobModel job, List<FlexworkerModel> flexworkers)
        {
            List<ResultModel> results = new List<ResultModel>();

            foreach (FlexworkerModel flexworker in flexworkers)
            {
                bool match = true;
                float totalWeight = 0;
                float weight = 0;

                foreach (PreferenceModel preference in job.Preferences)
                {
                    totalWeight += preference.Weight;

                    if (flexworker.Skills.Any(s => s.Id == preference.SkillId))
                    {
                        weight += preference.Weight;
                    }
                    else if (preference.IsRequired)
                    {
                        match = false;
                        break;
                    }
                }

                if (match)
                {
                    results.Add(new ResultModel
                    {
                        FlexworkerId = flexworker.Id,
                        Compatibility = (weight / totalWeight * 100)
                    });
                }
            }
            return results;
        }
    }
}
