using Aparta.Authentication.Application.Interfaces;

namespace Aparta.Authentication.Infra.Data.EF;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApartaAccountDbContext _context;

    public UnitOfWork(ApartaAccountDbContext context)
        => _context = context;

    public Task Commit(CancellationToken cancellationToken)
        => _context.SaveChangesAsync(cancellationToken);
}
