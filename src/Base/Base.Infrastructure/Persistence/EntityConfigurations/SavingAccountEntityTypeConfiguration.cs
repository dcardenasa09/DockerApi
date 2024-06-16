using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Base.Domain.AggregatesModel.SavingAccountAggregate;

namespace Base.Infrastructure.Persistence.EntityConfigurations;

public class SavingAccountEntityTypeConfiguration : IEntityTypeConfiguration<SavingAccount> {

	public void Configure(EntityTypeBuilder<SavingAccount> builder) {

		builder.ToTable("SavingAccounts");
		builder.HasKey(p => p.Id);

		builder.Property(b => b.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();
		builder.Property(b => b.ClientId).HasColumnName("client_id").IsRequired();
		builder.Property(b => b.AccountNumber).HasColumnName("account_number").IsRequired();
		builder.Property(b => b.Balance).HasColumnName("balance");
		builder.Property(b => b.OpeningDate).HasColumnName("opening_date").HasDefaultValue(DateTime.MinValue.ToUniversalTime());
		builder.Property(b => b.IsActive).HasColumnName("is_active").HasDefaultValue(true);

		builder.HasOne(x => x.Client)
			   .WithMany(x => x.SavingAccounts)
			   .HasForeignKey(x => x.ClientId);

		builder.HasMany(x => x.Transactions)
			   .WithOne(x => x.SavingAccount);
	}
}