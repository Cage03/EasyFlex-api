namespace Interface.Dtos
{
    public record JobResult
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public double Compatibility { get; init; }
        public List<Skill> CommonSkills { get; init; } = new List<Skill>();
    }
}
