namespace Aparta.Authentication.Application.Interfaces;

public interface IUnitOfWork
{
    public Task Commit(CancellationToken cancellationToken);
}
