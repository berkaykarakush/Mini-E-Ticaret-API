﻿using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Application.Repositories
{
    public interface ICompletedOrderWriteRepository: IWriteRepository<CompletedOrder>
    {
    }
}