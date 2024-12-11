using Microsoft.EntityFrameworkCore;

namespace consumer.DataAccess.PostgreSQL
{
    public class ContractDBContext(DbContextOptions<ContractDBContext> options) : DbContext(options)
    {
        public DbSet<Contract> Contracts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.ToTable("contracts");

                entity.HasKey(e => e.ContractId)
                      .HasName("PK_contracts");

                entity.Property(e => e.ContractId)
                      .HasColumnName("contractid")
                      .IsRequired()
                      .ValueGeneratedNever();

                entity.Property(e => e.Name)
                      .HasColumnName("name")
                      .HasMaxLength(30)
                      .IsRequired();
            });
        }
    }
}
