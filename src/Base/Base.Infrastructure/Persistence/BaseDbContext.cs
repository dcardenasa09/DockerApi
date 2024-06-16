using Microsoft.EntityFrameworkCore;
using Base.Domain.AggregatesModel.UserAggregate;
using Base.Domain.AggregatesModel.ClientAggregate;
using Base.Domain.AggregatesModel.TransactionAggregate;
using Base.Domain.AggregatesModel.SavingAccountAggregate;
using Base.Infrastructure.Persistence.EntityConfigurations;

namespace Base.Infrastructure.Persistence;

public class BaseDbContext(DbContextOptions<BaseDbContext> options) : DbContext(options) {
    public virtual DbSet<User>? Users { get; set; }
	public virtual DbSet<Client>? Clients { get; set; }
	public virtual DbSet<SavingAccount>? SavingsAccounts { get; set; }
	public virtual DbSet<Transaction>? Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
		modelBuilder.ApplyConfiguration(new ClientEntityTypeConfiguration());
		modelBuilder.ApplyConfiguration(new TransactionEntityTypeConfiguration());
		modelBuilder.ApplyConfiguration(new SavingAccountEntityTypeConfiguration());
	}
}