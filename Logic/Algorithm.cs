using Interface.Models;

namespace Logic
{
    public static class Algorithm
    {
        public static List<ResultModel> Execute(JobModel job, List<FlexworkerModel> flexworkers)
        {
            var results = new List<ResultModel>();

            foreach (FlexworkerModel flexworker in flexworkers)
            {
                var match = true;
                float totalWeight = 0;
                float weight = 0;

                foreach (var preference in job.Preferences)
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

                if (!match) continue;
                
                var compatibility = Math.Round(weight / totalWeight * 100);
                results.Add(new ResultModel
                {
                    FlexworkerId = flexworker.Id,
                    Compatibility = compatibility
                });
            }
            return results;
        }
    }
}
