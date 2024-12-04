namespace Interface.Dtos
{
    public record JobResult
    {
        public int JobId { get; init; }
        public required string JobName { get; init; }
        public double Compatibility { get; init; }
    }
}
