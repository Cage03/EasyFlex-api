﻿namespace Interface.Dtos;

public record Preference
{
    public int Id { get; set; }
    public int SkillId { get; set; }
    public int JobId { get; set; }

    public bool IsRequired { get; set; }
    public int Weight { get; set; }
}