using Heimdall.Modules.Users.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Heimdall.Modules.Users.Data.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        builder.Property(user => user.HashedPassword).HasMaxLength(512);
        builder.Property(user => user.Salt).HasMaxLength(128);
        builder.Property(user => user.CreatedBy).HasMaxLength(256);
        builder.Property(user => user.LastUpdatedBy).HasMaxLength(256);
        builder.HasQueryFilter(user => !user.IsDeleted);

        builder.OwnsOne(user => user.Profile, profile =>
        {
            profile.Property(entity => entity.UserName).HasMaxLength(64);
            profile.Property(entity => entity.FirstName).HasMaxLength(128);
            profile.Property(entity => entity.LastName).HasMaxLength(128);
            profile.Property(entity => entity.Email).HasMaxLength(320);
            profile.Property(entity => entity.PassportNumber).HasMaxLength(32);
            profile.HasIndex(entity => entity.UserName).IsUnique();
            profile.HasIndex(entity => entity.Email).IsUnique();
        });

        builder.OwnsOne(user => user.Address, address =>
        {
            address.Property(entity => entity.MainAddress).HasMaxLength(256);
            address.Property(entity => entity.SecondaryAddress).HasMaxLength(256);
            address.Property(entity => entity.PostalCode).HasMaxLength(16);
            address.Property(entity => entity.CountryCode).HasMaxLength(2).IsFixedLength();
            address.Property(entity => entity.City).HasMaxLength(128);
        });
    }
}
