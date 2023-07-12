using Aparta.Authentication.Infra.Data.EF;
using DomainEntity = Aparta.Authentication.Domain.Entity;

using Microsoft.EntityFrameworkCore;

namespace Aparta.Authentication.EndToEndTests.Api.Account.Common;

public class AccountPersistence
{
    private readonly ApartaAuthenticationDbContext _context;

    public AccountPersistence(ApartaAuthenticationDbContext context)
        => _context = context;

    public async Task<DomainEntity.Account?> GetById(Guid id)
        => await _context
            .Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task InserList(List<DomainEntity.Account> accounts)
    {
        await _context.Accounts.AddRangeAsync(accounts);
        await _context.SaveChangesAsync();
    }
}
