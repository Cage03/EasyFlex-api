﻿using Interface.Models;

namespace Interface.Interface.Dal;

public interface IJobDal
{
    public Task CreateJob(JobModel job);
}