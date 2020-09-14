using DeliveryConfirmation.Shared.Entities.Entities;
using System.Data.Entity;

namespace DeliveryConfirmation.Shared.Entities
{
    public partial class DeliveryDbContext : DbContext
    {
        public DeliveryDbContext()
            : base("DeliveryContext")
        {
        }

        public virtual DbSet<Drivers> Drivers { get; set; }
        public virtual DbSet<Trucks> Trucks { get; set; }
        public virtual DbSet<Shipments> Shipments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trucks>()
                .HasKey(t => t.TruckId)
                .Property(t => t.TruckId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Trucks>()
                .Property(t => t.TruckModel).HasMaxLength(100).IsRequired().IsUnicode();

            modelBuilder.Entity<Trucks>()
                .Property(t => t.TruckNumber).HasMaxLength(100).IsRequired().IsUnicode();

            modelBuilder.Entity<Trucks>()
                        .Property(t => t.CreationDate).IsRequired().HasColumnType("datetime");

            modelBuilder.Entity<Trucks>()
                        .Property(t => t.ModifiedDate).IsRequired().HasColumnType("datetime");

            modelBuilder.Entity<Shipments>()
                .HasKey(t => t.ShipmentId)
                .Property(t => t.CreationDate).IsRequired().HasColumnType("datetime"); //.HasDefaultValueSql("(getdate())");

            modelBuilder.Entity<Shipments>()
                .HasKey(t => t.ShipmentId)
                .Property(t => t.ModifiedDate).IsRequired().HasColumnType("datetime"); //.HasDefaultValueSql("(getdate())");

            modelBuilder.Entity<Shipments>()
                .Property(t => t.DestinationAddress).IsRequired().HasMaxLength(255).IsUnicode();

            modelBuilder.Entity<Shipments>()
                .Property(t => t.DestinationName).IsRequired().HasMaxLength(50).IsUnicode();

            modelBuilder.Entity<Shipments>()
                .Property(t => t.OriginAddress).IsRequired().HasMaxLength(255).IsUnicode();

            modelBuilder.Entity<Shipments>()
                .Property(t => t.OriginName).IsRequired().HasMaxLength(50).IsUnicode();

            modelBuilder.Entity<Shipments>()
                .Property(t => t.PackagesNumber).IsRequired();

            modelBuilder.Entity<Shipments>()
                .Property(t => t.StatusId).IsRequired();

            modelBuilder.Entity<Shipments>()
                .Property(t => t.IncidentalInformationId).IsRequired();

            modelBuilder.Entity<Shipments>()
                .Property(t => t.TrackingNumber).IsRequired();

            modelBuilder.Entity<Shipments>()
                .HasRequired(t => t.Truck)
                    .WithMany(t => t.Shipments)
                    .HasForeignKey(s => s.ShipmentId)
                    .WillCascadeOnDelete(false)
                    //.HasConstraintName("FK_Shipments_TruckId")
                    ;

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(DbModelBuilder modelBuilder);
    }
}