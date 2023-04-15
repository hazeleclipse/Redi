using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redi.Domain.Aggregates.StakerAggregate;
using Redi.Domain.Aggregates.StakerAggregate.ValueObjects;
using Redi.Domain.Common.Enumerations;

namespace Redi.Infrastructure.Persistence.EfCore.Configurations
{
    internal class StakerConfigurations : IEntityTypeConfiguration<Staker>
    {
        public void Configure(EntityTypeBuilder<Staker> builder)
        {
            builder.ToTable(nameof(Staker));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => StakerId.Create(value));

            builder.Property(s => s.Email)
                .HasConversion(
                    email => email.Value,
                    value => new EmailAddress(value))
                .HasMaxLength(100);

            builder.Property(s => s.FirstName)
                .HasConversion(
                    firstName => firstName.Value,
                    value => new FirstName(value))
                .HasMaxLength(100);

            builder.Property(s => s.LastName)
                .HasConversion(
                    lastName => lastName.Value,
                    value => new LastName(value))
                .HasMaxLength(100);

            builder.Property(s => s.Password)
                .HasConversion(
                    password => password.Value,
                    value => new Password(value));

            builder.Property(s => s.Role)
                .HasConversion(
                role => role.ToString(),
                varchar => (Role)Enum.Parse(typeof(Role), varchar))
                .HasMaxLength(50);

            builder.HasData(Staker.Create(Guid.NewGuid(), "admin@domain.com", "system", "admin" , BCrypt.Net.BCrypt.HashPassword("password"), Role.Admin));
        }
    }
}
