using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Models
{
    public class ResultModel
    {
        public int FlexworkerId { get; set; }
        public string FlexworkerName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public double Compatibility { get; set; }
    }
}
