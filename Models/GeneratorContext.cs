using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataGenerator.Models
{
    public partial class GeneratorContext : DbContext
    {
        public GeneratorContext()
        {
        }

        public GeneratorContext(DbContextOptions<GeneratorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Options> Options { get; set; }
        public virtual DbSet<TypeOptions> TypeOptions { get; set; }
        public virtual DbSet<Types> Types { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-U4NM3DU;Database=Generator;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Options>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.OptionName)
                    .HasColumnName("Option_name")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<TypeOptions>(entity =>
            {
                entity.HasKey(e => new { e.TypeId, e.OptionsId })
                    .HasName("PK__TypeOpti__02599097487654A9");

                entity.HasOne(d => d.Options)
                    .WithMany(p => p.TypeOptions)
                    .HasForeignKey(d => d.OptionsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TypeOptio__Optio__440B1D61");
            });

            modelBuilder.Entity<Types>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });
        }
    }
}
