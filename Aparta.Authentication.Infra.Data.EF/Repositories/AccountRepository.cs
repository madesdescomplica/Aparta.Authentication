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
        var account = await _accounts.AsNoTracking().FirstOrDefaultAsync(
            x => x.Id == id,
            cancellationToken
        );

        NotFoundException.ThrowIfNull(account, $"Account '{id}' not found.");
        return account!;
    }

    public Task Update(Account aggregate, CancellationToken _)
        => Task.FromResult(_accounts.Update(aggregate));

    public Task Delete(Account aggregate, CancellationToken _)
        => Task.FromResult(_accounts.Remove(aggregate));
}