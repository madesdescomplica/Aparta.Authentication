using Aparta.Authentication.Domain.Entity;
using Aparta.Authentication.Infra.Data.EF.Configurations;

using Microsoft.EntityFrameworkCore;

namespace Aparta.Authentication.Infra.Data.EF;

public class ApartaAuthenticationDbContext : DbContext
{
    public DbSet<Account> Accounts => Set<Account>();

    public ApartaAuthenticationDbContext(
        DbContextOptions<ApartaAuthenticationDbContext> options
    ) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AccountConfiguration());
    }
}