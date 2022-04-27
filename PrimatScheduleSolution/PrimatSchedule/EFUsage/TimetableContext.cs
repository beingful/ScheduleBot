using Microsoft.EntityFrameworkCore;

namespace PrimatScheduleBot
{
    public partial class TimetableContext : DbContext
    {
        public TimetableContext() {}

        public TimetableContext(DbContextOptions<TimetableContext> options) : base(options) {}

        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<MailingList> MailingLists { get; set; }
        public virtual DbSet<Periodicity> Periodicities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Data.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable(nameof(Event));

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ChatId)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Initiator).HasMaxLength(250);

                entity.Property(e => e.Link).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.PeriodicityId).HasDefaultValueSql("('4d46f7b3-78e1-4cf2-bd0d-2f227f24c776')");

                entity.Property(e => e.Place).HasMaxLength(250);

                entity.HasOne(d => d.Periodicity)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.PeriodicityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Event__Periodici__68487DD7");
            });

            modelBuilder.Entity<MailingList>(entity =>
            {
                entity.ToTable(nameof(MailingList));

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ChatId)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<Periodicity>(entity =>
            {
                entity.ToTable(nameof(Periodicity));

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
