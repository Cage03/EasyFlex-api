﻿using Interface.Dtos;

namespace Interface.Interface.Handlers;

public interface IMatchingHandler
{
    public Task<List<FlexworkerResult>> GetMatchesForJob(int jobId);
}