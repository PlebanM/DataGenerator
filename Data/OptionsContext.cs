using DataGenerator.Models.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Data
{
    public class OptionsContext : DbContext
    {

        public OptionsContext(DbContextOptions<OptionsContext> options) : base(options)
        {

        }

        public DbSet<ColumnType> ColumnTypes { get; set; }
        public DbSet<ColumnTypeOption> ColumnTypeOptions { get; set; }
        public DbSet<Option> Options { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ColumnTypeOption>()
                .HasKey(cto => new { cto.ColumnTypeId, cto.OptionId });
            modelBuilder.Entity<ColumnTypeOption>()
                .HasOne(cto => cto.ColumnType)
                .WithMany(ct => ct.ColumnTypeOptions)
                .HasForeignKey(cto => cto.ColumnTypeId);
            modelBuilder.Entity<ColumnTypeOption>()
                .HasOne(cto => cto.Option)
                .WithMany(o => o.ColumnTypeOptions)
                .HasForeignKey(cto => cto.OptionId);
        }
    }
}
