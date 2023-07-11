using Aparta.Authentication.Domain.Entity;
using Aparta.Authentication.Domain.Repository;

using Aparta.Authentication.Application.Exceptions;

using Microsoft.EntityFrameworkCore;

namespace Aparta.Authentication.Infra.Data.EF.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly ApartaAuthenticationDbContext _context;

    private DbSet<Account> _accounts
        => _context.Set<Account>();

    public AccountRepository(ApartaAuthenticationDbContext context)
        => _context = context;

    public async Task Insert(
        Account aggregate,
        CancellationToken cancellationToken
    )
        => await _accounts.AddAsync(aggregate, cancellationToken);

    public async Task<Account> Get(Guid id, CancellationToken cancellationToken)
    {
        var category = await _accounts.AsNoTracking().FirstOrDefaultAsync(
            x => x.Id == id,
            cancellationToken
        );
        NotFoundException.ThrowIfNull(category, $"Account '{id}' not found.");
        return category!;
    }
}