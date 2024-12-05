using Interface.Dtos;

namespace Logic
{
    public static class Algorithm
    {

        public static List<JobResult> FindJobsForFlexworker(Flexworker flexworker, List<Job> jobs)
        {
            var results = new List<JobResult>();

            foreach (Job job in jobs)
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
                results.Add(new JobResult
                {
                    JobId = job.Id,
                    JobName = job.Name,
                    Compatibility = compatibility
                });
            }

            return results;
        }

        public static List<FlexworkerResult> FindFlexworkersForJob(Job job, List<Flexworker> flexworkers)
        {
            var results = new List<FlexworkerResult>();
            if (job.Preferences.Count == 0) return results;
            
            foreach (Flexworker flexworker in flexworkers)
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
                
                int compatibility = (int)Math.Round(weight / totalWeight * 100);
                results.Add(new FlexworkerResult
                {
                    FlexworkerId = flexworker.Id,
                    Name = flexworker.Name,
                    ProfilePictureUrl = flexworker.ProfilePictureUrl,
                    Compatibility = compatibility
                });
            }
            return results;
        }
    }
}
