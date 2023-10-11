using DataHandler.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler.CdrDbContext.FluentConfiguration
{
    public class CallDetailRecordDBConfiguration : IEntityTypeConfiguration<CallDetailRecord>
    {
        public void Configure(EntityTypeBuilder<CallDetailRecord> builder)
        {
            builder.ToTable("call_detail_record"); // Set the table name

            // Map the properties to columns in the database
            //builder.HasKey(e => e.Id);
            builder.HasKey(e => e.Reference);

            //builder.Property(e => e.Id).ValueGeneratedOnAdd();
            //builder.Property(e => e.Id).HasColumnName("id").IsRequired();

            builder.Property(e => e.CallerId).HasColumnName("caller_id").HasMaxLength(20).IsRequired();
            builder.Property(e => e.Recipient).HasColumnName("recipient").HasMaxLength(20).IsRequired();
            builder.Property(e => e.CallDate).HasColumnName("call_date").IsRequired();
            builder.Property(e => e.EndTime).HasColumnName("end_time").IsRequired();
            builder.Property(e => e.Duration).HasColumnName("duration").IsRequired();
            builder.Property(e => e.Cost).HasColumnName("cost").IsRequired().HasPrecision(18,3);
            builder.Property(e => e.Reference).HasColumnName("reference").IsRequired();            
            builder.Property(e => e.Currency).HasColumnName("currency").IsRequired().HasMaxLength(3);

            //builder.HasIndex(e => e.Reference).IsUnique();
        }
    }
}
