using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Base.Domain.AggregatesModel.UserAggregate;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Base.Infrastructure.Persistence.EntityConfigurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User> {

	public void Configure(EntityTypeBuilder<User> builder) {

		builder.ToTable("Users");
		builder.HasKey(p => p.Id);

		builder.Property(b => b.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();
		builder.Property(b => b.Name).HasColumnName("name").IsRequired();
		builder.Property(b => b.LastName).HasColumnName("last_name").IsRequired();
		builder.Property(b => b.Password).HasColumnName("password").IsRequired();
		builder.Property(b => b.Email).HasColumnName("email").IsRequired();
		builder.Property(b => b.RefreshToken).HasColumnName("refresh_token").HasDefaultValue(string.Empty);
		builder.Property(b => b.RefreshTokenExpiresAt).HasColumnName("refresh_token_expires_at").HasDefaultValue(DateTime.MinValue.ToUniversalTime());

		builder.HasData(
			new User { Id = 1, Name = "Jhon", LastName = "Doe", Email = "admin@test.com", Password = BCryptNet.HashPassword("Test2024.") }
		);
	}
}