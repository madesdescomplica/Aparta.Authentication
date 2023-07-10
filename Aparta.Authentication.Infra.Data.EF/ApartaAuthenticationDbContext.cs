using Aparta.Authentication.Domain.Entity;
using Aparta.Authentication.Infra.Data.EF.Configurations;

using Microsoft.EntityFrameworkCore;

namespace Aparta.Authentication.Infra.Data.EF;

public class ApartaAccountDbContext : DbContext
{
    public DbSet<Account> Accounts => Set<Account>();

    public ApartaAccountDbContext(
        DbContextOptions<ApartaAccountDbContext> options
    ) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AccountConfiguration());
    }
}