using Interface.Models;

namespace Logic
{
    public static class Algorithm
    {

        public static List<JobResultModel> FindJobsForFlexworker(FlexworkerModel flexworker, List<JobModel> jobs)
        {
            var results = new List<JobResultModel>();

            foreach (JobModel job in jobs)
            {
                var match = true;
                float totalWeight = 0;
                float weight = 0;

                foreach (var preference in job.Preferences)
                {
                    totalWeight += preference.Weight;

                    if (preference.IsRequired)
                    {
                        if (!flexworker.Skills.Any(s => s.Id == preference.SkillId))
                        {
                            match = false;
                            break;
                        }

                        weight += preference.Weight;
                    }
                    else if (flexworker.Skills.Any(s => s.Id == preference.SkillId))
                    {
                        weight += preference.Weight;
                    }
                }

                if (!match || totalWeight == 0 || weight == 0) continue;

                var compatibility = Math.Round(weight / totalWeight * 100);
                results.Add(new JobResultModel
                {
                    JobId = job.Id,
                    JobName = job.Name,
                    Compatibility = compatibility
                });
            }

            return results;
        }




        public static List<FlexworkerResultModel> FindFlexworkersForJob(JobModel job, List<FlexworkerModel> flexworkers)
        {
            var results = new List<FlexworkerResultModel>();
        
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
                results.Add(new FlexworkerResultModel
                {
                    FlexworkerId = flexworker.Id,
                    FlexworkerName = flexworker.Name,
                    ProfilePictureUrl = flexworker.ProfilePictureUrl,
                    Compatibility = compatibility
                });
            }
            return results;
        }
    }
}
