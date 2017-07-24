using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace myScheduleModels.Models
{
    public partial class myScheduleContext : DbContext
    {
        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<Lists> Lists { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<ScheduledEvent> ScheduledEvent { get; set; }

        private IConfigurationRoot _config;

        public myScheduleContext(DbContextOptions options, IConfigurationRoot config)
          : base(options)
        {
            _config = config;
        }

        //   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //   {
        ////       #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //       optionsBuilder.UseSqlServer(@"Server=OFFICE_STAN\SQLEXPRESS;Database=mySchedule;Trusted_Connection=True;");
        //   }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<History>(entity =>
        //    {
        //        entity.ToTable("history");

        //        entity.Property(e => e.Id)
        //            .HasColumnName("ID")
        //            .ValueGeneratedNever();

        //        entity.Property(e => e.PreviousId).HasColumnName("previousID");

        //        entity.Property(e => e.Reason).HasColumnName("reason");

        //        entity.Property(e => e.Status).HasColumnName("status");

        //        entity.Property(e => e.UpdatedOn)
        //            .HasColumnName("updatedOn")
        //            .HasColumnType("datetime");
        //    });

        //    modelBuilder.Entity<Lists>(entity =>
        //    {
        //        entity.HasKey(e => e.ListId)
        //            .HasName("PK_lists");

        //        entity.ToTable("lists");

        //        entity.Property(e => e.ListId).HasColumnName("listID");

        //        entity.Property(e => e.Label)
        //            .IsRequired()
        //            .HasColumnName("label")
        //            .HasMaxLength(50);

        //        entity.Property(e => e.ListName)
        //            .IsRequired()
        //            .HasColumnName("listName")
        //            .HasMaxLength(50);

        //        entity.Property(e => e.Value).HasColumnName("value");
        //    });

        //    modelBuilder.Entity<Location>(entity =>
        //    {
        //        entity.Property(e => e.Id).HasColumnName("ID");

        //        entity.Property(e => e.Address)
        //            .HasColumnName("address")
        //            .HasMaxLength(64);

        //        entity.Property(e => e.Description).HasColumnName("description");

        //        entity.Property(e => e.Lat)
        //            .HasColumnName("lat")
        //            .HasColumnType("decimal");

        //        entity.Property(e => e.Lon)
        //            .HasColumnName("lon")
        //            .HasColumnType("decimal");

        //        entity.Property(e => e.Path).HasColumnName("path");
        //    });

        //    modelBuilder.Entity<ScheduledEvent>(entity =>
        //    {
        //        entity.Property(e => e.Id).HasColumnName("ID");

        //        entity.Property(e => e.BeginAt)
        //            .HasColumnName("beginAt")
        //            .HasColumnType("datetime");

        //        entity.Property(e => e.Duration).HasColumnName("duration");

        //        entity.Property(e => e.History).HasColumnName("history");

        //        entity.Property(e => e.LocationId).HasColumnName("locationID");

        //        entity.Property(e => e.Purpose).HasColumnName("purpose");

        //        entity.Property(e => e.ScheduleUser)
        //            .IsRequired()
        //            .HasColumnName("scheduleUser")
        //            .HasMaxLength(450);

        //        entity.HasOne(d => d.Location)
        //            .WithMany(p => p.ScheduledEvent)
        //            .HasForeignKey(d => d.LocationId)
        //            .OnDelete(DeleteBehavior.Restrict)
        //            .HasConstraintName("FK_ScheduledEvent_Location");
        //    });
        //}
    }
}