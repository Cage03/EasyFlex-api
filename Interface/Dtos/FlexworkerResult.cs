using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Dtos
{
    public record FlexworkerResult
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public string? ProfilePictureUrl { get; init; }
        public int Compatibility { get; init; }
    }
}
