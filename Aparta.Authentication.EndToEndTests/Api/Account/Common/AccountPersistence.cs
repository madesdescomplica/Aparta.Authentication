using Aparta.Authentication.Infra.Data.EF;

using Microsoft.EntityFrameworkCore;

namespace Aparta.Authentication.EndToEndTests.Api.Account.Common;

public class AccountPersistence
{
    private readonly ApartaAuthenticationDbContext _context;

    public AccountPersistence(ApartaAuthenticationDbContext context)
        => _context = context;

    public async Task<Domain.Entity.Account?> GetById(Guid id)
        => await _context
            .Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
}
