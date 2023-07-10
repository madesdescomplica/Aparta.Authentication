﻿namespace Aparta.Authentication.Domain.SeedWork;

public interface IGenericRepository<TAggregate> : IRepository
    where TAggregate : AggregateRoot
{
    public Task Insert(
        TAggregate aggregate,
        CancellationToken cancellationToken
    );
}
