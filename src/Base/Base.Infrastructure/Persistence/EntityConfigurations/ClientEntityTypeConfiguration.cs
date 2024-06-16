using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Base.Domain.AggregatesModel.ClientAggregate;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Base.Infrastructure.Persistence.EntityConfigurations;

public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client> {

	public void Configure(EntityTypeBuilder<Client> builder) {

		builder.ToTable("Clients");
		builder.HasKey(p => p.Id);

		builder.Property(b => b.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();
		builder.Property(b => b.Name).HasColumnName("name").IsRequired();
		builder.Property(b => b.LastName).HasColumnName("last_name").IsRequired();
		builder.Property(b => b.SecondLastName).HasColumnName("second_last_name").IsRequired();
		builder.Property(b => b.Email).HasColumnName("email").IsRequired();
		builder.Property(b => b.Phone).HasColumnName("phone").IsRequired();
		builder.Property(b => b.Birthdate).HasColumnName("birth_date").HasDefaultValue(DateTime.MinValue.ToUniversalTime());
		builder.Property(b => b.IsActive).HasColumnName("is_active").HasDefaultValue(true);

		builder.HasMany(x => x.SavingAccounts)
			   .WithOne(x => x.Client);
	}
}