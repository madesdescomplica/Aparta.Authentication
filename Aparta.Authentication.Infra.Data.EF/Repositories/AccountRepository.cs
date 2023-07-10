using Aparta.Authentication.Domain.Entity;
using Aparta.Authentication.Domain.Repository;

using Microsoft.EntityFrameworkCore;

namespace Aparta.Authentication.Infra.Data.EF.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly ApartaAccountDbContext _context;

    private DbSet<Account> _accounts
        => _context.Set<Account>();

    public AccountRepository(ApartaAccountDbContext context)
        => _context = context;

    public async Task Insert(
        Account aggregate,
        CancellationToken cancellationToken
    )
        => await _accounts.AddAsync(aggregate, cancellationToken);
}