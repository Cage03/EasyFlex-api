using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Models
{
    public class PreferenceModel
    {
        public int Id { get; set; }
        public int SkillId { get; set; }
        public int JobId { get; set; }

        public bool IsRequired { get; set; }
        public int Weight { get; set; }
    }
}
