﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Dtos
{
    public record FlexworkerResult
    {
        public int FlexworkerId { get; init; }
        public required string Name { get; init; }
        public string? ProfilePictureUrl { get; init; }
        public double Compatibility { get; init; }
        public ICollection<Skill> Skills { get; init; } = new List<Skill>();
    }
}
