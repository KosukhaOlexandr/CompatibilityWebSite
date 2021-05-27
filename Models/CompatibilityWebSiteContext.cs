using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CompatibilityWebSite
{
    public partial class CompatibilityWebSiteContext : DbContext
    {
        public CompatibilityWebSiteContext()
        {
        }

        public CompatibilityWebSiteContext(DbContextOptions<CompatibilityWebSiteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActiveSubstance> ActiveSubstances { get; set; }
        public virtual DbSet<Compatibility> Compatibilities { get; set; }
        public virtual DbSet<CompatibilityStatus> CompatibilityStatuses { get; set; }
        public virtual DbSet<Desease> Deseases { get; set; }
        public virtual DbSet<DeseasesActiveSubstance> DeseasesActiveSubstances { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<MedicineActiveSubstance> MedicineActiveSubstances { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= DESKTOP-TKKMN11; Database=CompatibilityWebSite; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Ukrainian_CI_AS");

            modelBuilder.Entity<ActiveSubstance>(entity =>
            {
                entity.ToTable("Active_Substance");

                entity.Property(e => e.Info).HasMaxLength(1000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Compatibility>(entity =>
            {
                entity.ToTable("Compatibility");

                entity.Property(e => e.CompatibilityStatusId).HasColumnName("Compatibility_Status_Id");

                entity.Property(e => e.FirstActiveSubstance).HasColumnName("First_Active_Substance");

                entity.Property(e => e.SecondActiveSubstance).HasColumnName("Second_Active_Substance");

                entity.HasOne(d => d.CompatibilityStatus)
                    .WithMany(p => p.Compatibilities)
                    .HasForeignKey(d => d.CompatibilityStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Compatibility_Compatibility_Status");

                entity.HasOne(d => d.FirstActiveSubstanceNavigation)
                    .WithMany(p => p.CompatibilityFirstActiveSubstanceNavigations)
                    .HasForeignKey(d => d.FirstActiveSubstance)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Compatibility_Active_Substance");

                entity.HasOne(d => d.SecondActiveSubstanceNavigation)
                    .WithMany(p => p.CompatibilitySecondActiveSubstanceNavigations)
                    .HasForeignKey(d => d.SecondActiveSubstance)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Compatibility_Active_Substance1");
            });

            modelBuilder.Entity<CompatibilityStatus>(entity =>
            {
                entity.ToTable("Compatibility_Status");

                entity.Property(e => e.Info).HasMaxLength(175);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Desease>(entity =>
            {
                entity.ToTable("Desease");

                entity.Property(e => e.Info).HasMaxLength(1000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<DeseasesActiveSubstance>(entity =>
            {
                entity.ToTable("DeseasesActive_Substances");

                entity.Property(e => e.ActiveSubstanceId).HasColumnName("Active_SubstanceId");

                entity.HasOne(d => d.ActiveSubstance)
                    .WithMany(p => p.DeseasesActiveSubstances)
                    .HasForeignKey(d => d.ActiveSubstanceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeseasesActive_Substances_Active_Substance");

                entity.HasOne(d => d.Desease)
                    .WithMany(p => p.DeseasesActiveSubstances)
                    .HasForeignKey(d => d.DeseaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeseasesActive_Substances_Desease");
            });

            modelBuilder.Entity<MedicineActiveSubstance>(entity =>
            {
                entity.ToTable("MedicineActive_Substances");

                entity.Property(e => e.ActiveSubtanceId).HasColumnName("Active_SubstanceId");

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.MedicineActiveSubstances)
                    .HasForeignKey(d => d.MedicineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MedicineActive_Substances_Medicine");

                entity.HasOne(p => p.ActiveSubstance)
                    .WithMany(d => d.MedicineActiveSubstances)
                    .HasForeignKey(d => d.ActiveSubtanceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MedicineActive_Substances_Active_Substance");
            });

            modelBuilder.Entity<Medicine>(entity =>
            {
                entity.ToTable("Medicine");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Instruction)
                    .HasMaxLength(1000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
