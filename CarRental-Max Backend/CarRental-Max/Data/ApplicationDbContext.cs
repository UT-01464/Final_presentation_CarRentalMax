using CAR_RENTAL_MS_III.Entities;
using CarRental_Max.Entities;
using CarRental_Max.Entities.CarDetails;
using Microsoft.EntityFrameworkCore;

namespace CAR_RENTAL_MS_III.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public DbSet<CarCategory> CarCategories { get; set; }
       
        public DbSet<User> Users { get; set; }


        public DbSet<Model> Models { get; set; }

        public DbSet<Transmission> Transmissions { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Seat> Seats { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }
        



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                 .HasOne(c => c.Model)
       .WithMany(m => m.Cars)
       .HasForeignKey(c => c.ModelId)
       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Category)
                .WithMany(cc => cc.Cars)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Transmission)
                .WithMany()
                .HasForeignKey(c => c.TransmissionId);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.FuelType)
                .WithMany()
                .HasForeignKey(c => c.FuelTypeId);

           

            modelBuilder.Entity<Car>()
                .HasMany(c => c.Seats)
                .WithOne()
                .HasForeignKey("CarId");


            modelBuilder.Entity<Notification>()
            .HasOne(n => n.Customer)
            .WithMany(c => c.Notifications)
            .HasForeignKey(n => n.CustomerId);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Rental)
                .WithMany()
                .HasForeignKey(n => n.RentalId)
                .OnDelete(DeleteBehavior.Restrict);

        }




    }
}
