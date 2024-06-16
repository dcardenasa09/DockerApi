using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Base.Domain.AggregatesModel.TransactionAggregate;

namespace Base.Infrastructure.Persistence.EntityConfigurations;

public class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<Transaction> {

	public void Configure(EntityTypeBuilder<Transaction> builder) {

		builder.ToTable("Transactions");
		builder.HasKey(p => p.Id);

		builder.Property(b => b.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();
		builder.Property(b => b.Folio).HasColumnName("folio").IsRequired();
		builder.Property(b => b.SavingAccountId).HasColumnName("saving_account_id").IsRequired();
		builder.Property(b => b.Amount).HasColumnName("amount").HasColumnType("decimal(18, 2)");
		builder.Property(b => b.Date).HasColumnName("date").HasDefaultValue(DateTime.MinValue.ToUniversalTime());
		builder.Property(b => b.Type).HasColumnName("type");
		builder.Property(b => b.Status).HasColumnName("status");
		builder.Property(b => b.IsActive).HasColumnName("is_active").HasDefaultValue(true);

		builder.HasOne(x => x.SavingAccount)
			   .WithMany(x => x.Transactions)
			   .HasForeignKey(x => x.SavingAccountId);
	}
}