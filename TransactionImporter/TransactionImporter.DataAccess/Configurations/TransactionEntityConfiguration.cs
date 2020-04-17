using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TransactionImporter.DataAccess.Entities;

namespace DummyMvc.DataAccess.EntitiesConfigurations
{
    internal class TransactionEntityConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder
                .HasKey(a => a.Id);
            builder
                .Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.CurrencyCode).IsRequired();
            builder.Property(e => e.TransactionDate).IsRequired();
            builder.Property(e => e.Amount).IsRequired();
            builder
                .Property(a => a.TransactionStatus)
                .IsRequired()
                .HasConversion(e => e.ToString(), e => (TransactionStatusEnum)Enum.Parse(typeof(TransactionStatusEnum), e));
            
        }
    }
}