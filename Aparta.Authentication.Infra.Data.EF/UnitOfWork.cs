using Aparta.Authentication.Application.Interfaces;

namespace Aparta.Authentication.Infra.Data.EF;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApartaAuthenticationDbContext _context;

    public UnitOfWork(ApartaAuthenticationDbContext context)
        => _context = context;

    public Task Commit(CancellationToken cancellationToken)
        => _context.SaveChangesAsync(cancellationToken);
}
