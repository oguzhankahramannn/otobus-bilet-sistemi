using Microsoft.EntityFrameworkCore;
using OtobusBiletiApp.Models;

namespace OtobusBiletiApp
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Bus> Buses { get; set; } // ✔️ düzelttik
        public DbSet<BusCompany> Companies { get; set; }
        public DbSet<CompanyTel> CompanyTels { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<BusFeature> BusFeatures { get; set; } // ✔️ doğru olan bu
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<PaymentProcessing> Payments { get; set; }
        public DbSet<TicketSeat> TicketSeats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // BusFeature -> Composite Key
            modelBuilder.Entity<BusFeature>().HasKey(bf => new { bf.b_plaka, bf.feature_name });

            modelBuilder
                .Entity<BusFeature>()
                .HasOne(bf => bf.Bus)
                .WithMany()
                .HasForeignKey(bf => bf.b_plaka);

            // TicketSeat ilişkileri
            modelBuilder
                .Entity<TicketSeat>()
                .HasOne(ts => ts.Ticket)
                .WithMany()
                .HasForeignKey(ts => ts.PNR_NO);

            modelBuilder
                .Entity<TicketSeat>()
                .HasOne(ts => ts.Seat)
                .WithMany()
                .HasForeignKey(ts => ts.seat_no);

            modelBuilder
                .Entity<TicketSeat>()
                .HasOne(ts => ts.Bus)
                .WithMany()
                .HasForeignKey(ts => ts.b_plaka);

            // Trip ilişkileri
            modelBuilder
                .Entity<Trip>()
                .HasOne(t => t.Bus)
                .WithMany()
                .HasForeignKey(t => t.b_plaka);

            modelBuilder.Entity<Trip>().HasOne(t => t.Person).WithMany().HasForeignKey(t => t.p_id);

            // Seat ilişkileri
            modelBuilder
                .Entity<Seat>()
                .HasOne(s => s.Bus)
                .WithMany()
                .HasForeignKey(s => s.b_plaka);

            modelBuilder
                .Entity<Seat>()
                .HasOne(s => s.Ticket)
                .WithMany()
                .HasForeignKey(s => s.PNR_NO);

            modelBuilder.Entity<Seat>().HasOne(s => s.Person).WithMany().HasForeignKey(s => s.p_id);

            // Ticket ilişkileri
            modelBuilder
                .Entity<Ticket>()
                .HasOne(t => t.Trip)
                .WithMany()
                .HasForeignKey(t => t.trip_id);

            modelBuilder
                .Entity<Ticket>()
                .HasOne(t => t.Person)
                .WithMany()
                .HasForeignKey(t => t.p_id);

            modelBuilder
                .Entity<Ticket>()
                .HasOne(t => t.PaymentProcessing)
                .WithMany()
                .HasForeignKey(t => t.payment_id);
        }
    }
}
