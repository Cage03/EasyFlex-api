using Interface.Dtos;

namespace Test.AlgorithmTests
{
    public class FlexworkerGenerator
    {
        private List<Flexworker> flexworkers;

        public FlexworkerGenerator(List<Skill> skills)
        {
            flexworkers = new List<Flexworker>();

            var flexworkerData = new List<(int Id, string Name, List<int> SkillIndices)>
            {
                (0, "Nederlandstalig", new List<int> { 0 }),
                (1, "Geen skills",new List<int>()),
                (2, "Nederlands en engels",new List < int > { 0, 1 }),
                (3, "Engels",new List < int > { 1 }),
                (4, "Nederlands en Havodiploma",new List < int > { 0, 2}),
                (5, "Nederlands, Engels, Havodiploma",new List < int > { 0, 1, 2}),
                (6, "Nederlands, engels, havo en heftruck", new List < int > { 0, 1, 2, 3}),
                (7, "Nederlands, havo en heftruck", new List < int > { 0, 2, 3}),
                (8, "engels en havo", new List < int > { 1, 2}),
                (9, "havo", new List<int> { 2 }),
            };

            foreach (var data in flexworkerData)
            {
                var flexworker = new Flexworker
                {
                    Id = data.Id,
                    Name = data.Name,
                    Email = "email",
                    PhoneNumber = "0612345678",
                    DateOfBirth = new DateOnly(1990, 10, 1),
                    Address = "Adress",
                    ProfilePictureUrl = "url",
                };

                foreach (var skillIndex in data.SkillIndices)
                {
                    flexworker.Skills.Add(skills[skillIndex]);
                }

                flexworkers.Add(flexworker);
            }
        }
        public Flexworker Flexworker_Sc_1()
        {
            return flexworkers[0];
        }
        
        public Flexworker Flexworker_Sc_2()
        {
            return flexworkers[2];
        }

        public List<Flexworker> Flexworkers_Sc_1_3()
        {
            List<Flexworker> output = new List<Flexworker>();
            output.Add(flexworkers[0]);
            output.Add(flexworkers[1]);

            return output;
        }

        public List<Flexworker> Flexworkers_Sc_2_4()
        {
            List<Flexworker> output = new List<Flexworker>();

            //Nederlands en engels
            output.Add(flexworkers[2]);
            //Nederlandstalig
            output.Add(flexworkers[0]);
            //Engels
            output.Add(flexworkers[3]);
            //Geen skills
            output.Add(flexworkers[1]);

            return output;
        }

        public List<Flexworker> Flexworkers_Sc_5()
        {
            List<Flexworker> output = new List<Flexworker>();

            //Nederlands, Havodiploma, engels
            output.Add(flexworkers[5]);
            //Nederlands en Havodiploma
            output.Add(flexworkers[4]);
            //Nederlands en engels
            output.Add(flexworkers[2]);
            //Engels, Havodiploma
            output.Add(flexworkers[8]);
            //Havodiploma
            output.Add(flexworkers[9]);
            //Engels
            output.Add(flexworkers[3]);

            return output;
        }

        public List<Flexworker> Flexworkers_Sc_6()
        {
            List<Flexworker> output = new List<Flexworker>();
            //Nederlands en Engels
            output.Add(flexworkers[2]);
            //Nederlands
            output.Add(flexworkers[0]);
            //Geen skills
            output.Add(flexworkers[1]);

            return output;
        }

        public List<Flexworker> Flexworkers_Sc_7()
        {
            List<Flexworker> output = new List<Flexworker>();

            output.Add(flexworkers[5]);
            output.Add(flexworkers[4]);
            output.Add(flexworkers[2]);
            output.Add(flexworkers[0]);
            output.Add(flexworkers[3]);

            return output;
        }

        public List<Flexworker> Flexworkers_Sc_8()
        {
            List<Flexworker> output = new List<Flexworker>();
            output.Add(flexworkers[6]);
            output.Add(flexworkers[5]);
            output.Add(flexworkers[2]);
            output.Add(flexworkers[7]);
            output.Add(flexworkers[3]);
            return output;
        }
    }
}
