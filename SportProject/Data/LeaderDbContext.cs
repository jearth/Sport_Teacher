using Microsoft.EntityFrameworkCore;

namespace SportProject.Data
{
    public class LeaderDbContext : DbContext
    {
        public LeaderDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T_School>(option =>
            {
                option.HasKey(e => e.SchoolNo);
                option.HasMany(e => e.T_User)
                      .WithOne(e => e.T_School)
                      .HasForeignKey(e => e.SchoolNo);
            });

            modelBuilder.Entity<T_User>(option =>
            {
                option.HasKey(e => e.LeaderNo);
                option.HasMany(e => e.T_Work)
                      .WithOne()
                      .HasForeignKey(e => e.LeaderNo);

                option.HasMany(e => e.T_Certificates)
                      .WithOne()
                      .HasForeignKey(e => e.LeaderNo);

                option.HasOne(e => e.T_Image)
                     .WithOne()
                     .HasForeignKey<T_Image>(e => e.LeaderNo);
            });

            modelBuilder.Entity<T_Sport>(option =>
            {
                option.HasKey(e => e.SportNo);
                option.HasMany(e => e.T_User)
                      .WithOne(e=> e.T_Sport)
                      .HasForeignKey(e => e.SportNo);
            });

            modelBuilder.Entity<T_Leader>(option =>
            {
                option.HasKey(e => e.LeaderNo);
                option.HasOne(e => e.T_User)
                      .WithOne(e=> e.T_Leader)
                      .HasForeignKey<T_User>(e => e.LeaderNo);
            });

            modelBuilder.Entity<T_Work>(option =>
            {
                option.HasKey(e => new { e.No, e.LeaderNo });
                option.Property(e => e.No).UseIdentityColumn();
            });

            modelBuilder.Entity<T_Certificate>(option =>
            {
                option.HasKey(e => new { e.No, e.LeaderNo });
                option.Property(e => e.No).UseIdentityColumn();
            });


            modelBuilder.Entity<T_Image>(option =>
            {
                option.HasKey(e => e.LeaderNo);
            });
        }

        
        public DbSet<T_Certificate> T_Certificate { get; set; }
        public DbSet<T_Image> T_Image { get; set; }
        public DbSet<T_Leader> T_Leader { get; set; }
        public DbSet<T_School> T_School { get; set; }
        public DbSet<T_Sport> T_Sport { get; set; }
        public DbSet<T_User> T_User { get; set; }
        public DbSet<T_Work> T_Work { get; set; }
    }
}
