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
                List<Skill> compSkills = new List<Skill>();
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
                        compSkills.Add(flexworker.Skills.First(s => s.Id == preference.SkillId));
                    }
                    else if (flexworker.Skills.Any(s => s.Id == preference.SkillId))
                    {
                        weight += preference.Weight;
                        compSkills.Add(flexworker.Skills.First(s => s.Id == preference.SkillId));
                    }
                }

                if (!match || totalWeight == 0 || weight == 0) continue;

                var compatibility = Math.Round(weight / totalWeight * 100);
                results.Add(new JobResult
                {
                    Id = job.Id,
                    Name = job.Name,
                    Compatibility = compatibility,
                    CommonSkills = compSkills
                });
            }
            results.Sort((a, b) => b.Compatibility.CompareTo(a.Compatibility));
            return results;
        }

        public static List<FlexworkerResult> FindFlexworkersForJob(Job job, List<Flexworker> flexworkers)
        {
            var results = new List<FlexworkerResult>();
            
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

                int compatibility = 0;
                if (weight > 0)
                { 
                    compatibility = (int)Math.Round(weight / totalWeight * 100);
                }

                results.Add(new FlexworkerResult
                {
                    Id = flexworker.Id,
                    Name = flexworker.Name,
                    ProfilePictureUrl = flexworker.ProfilePictureUrl,
                    Compatibility = compatibility,
                    Skills = flexworker.Skills.Where(s => job.Preferences.Any(p => p.SkillId == s.Id)).ToList()
                });
            }
            results.Sort((a, b) => b.Compatibility.CompareTo(a.Compatibility));
            return results;
        }
    }
}
