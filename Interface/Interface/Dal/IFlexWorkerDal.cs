﻿using Interface.Models;

namespace Interface.Interface.Dal;

public interface IFlexWorkerDal
{
    public List<FlexworkerModel> GetAllFlexWorkers();
    public Task AddFlexWorker(FlexworkerModel flexWorker);

    public Task UpdateFlexWorker(FlexworkerModel flexWorker);
}