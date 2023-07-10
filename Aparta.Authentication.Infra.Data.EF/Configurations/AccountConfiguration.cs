using Aparta.Authentication.Domain.Entity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aparta.Authentication.Infra.Data.EF.Configurations;

public class AccountConfiguration
    : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(account => account.Id);
        builder.Property(account => account.Name)
            .HasMaxLength(255);
    }
}
