using AspReactTestApp.Entities.Concrete;
using AspReactTestApp.Entities.Concrete.CarRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AspReactTestApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() {}

        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region SettingRelations
            modelBuilder.Entity<Brand>()
              .HasMany<Car>()
              .WithOne(c => c.Brand)
              .HasForeignKey("BrandId")
              .IsRequired();

            modelBuilder.Entity<Model>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Brand>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Model>()
              .HasOne(m => m.Brand)
              .WithMany(b => b.Models)
              .HasForeignKey("BrandId")
              .OnDelete(DeleteBehavior.Cascade)
              .IsRequired();

            modelBuilder.Entity<Model>()
              .HasMany<Car>()
              .WithOne(c => c.Model)
              .HasForeignKey("ModelId")
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Model>()
              .HasOne(m => m.BaseModel)
              .WithMany()
              .HasForeignKey("BaseModelId")
              .IsRequired(false)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Category>()
              .HasMany<Car>()
              .WithOne(c => c.Category)
              .HasForeignKey("CategoryId")
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();

            modelBuilder.Entity<Category>()
             .HasMany(c => c.CategoryLocales)
             .WithOne(cl => cl.Category)
             .HasForeignKey("CategoryId")
             .OnDelete(DeleteBehavior.Restrict)
             .IsRequired();

            modelBuilder.Entity<CategoryLocale>()
            .HasOne(cl => cl.Language)
            .WithMany(l => l.CategoryLocales)
            .HasForeignKey("LanguageId")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

            modelBuilder.Entity<Region>()
              .HasMany<Car>()
              .WithOne(c => c.Region)
              .HasForeignKey("RegionId")
              .IsRequired();

            modelBuilder.Entity<Region>()
             .HasMany(r => r.RegionLocales)
             .WithOne(rl => rl.Region)
             .HasForeignKey("RegionId")
             .IsRequired();

            modelBuilder.Entity<RegionLocale>()
            .HasOne(rl => rl.Language)
            .WithMany(l => l.RegionLocales)
            .HasForeignKey("LanguageId")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

            modelBuilder.Entity<Fueltype>()
              .HasMany<Car>()
              .WithOne(c => c.Fueltype)
              .HasForeignKey("FueltypeId")
              .IsRequired();

            modelBuilder.Entity<Fueltype>()
              .HasMany(ft => ft.FueltypeLocales)
              .WithOne(ftl => ftl.Fueltype)
              .HasForeignKey("FueltypeId")
              .IsRequired();

            modelBuilder.Entity<FueltypeLocale>()
             .HasOne(ftl => ftl.Language)
             .WithMany(l => l.FueltypeLocales)
             .HasForeignKey("LanguageId")
             .IsRequired();

            modelBuilder.Entity<Color>()
              .HasMany<Car>()
              .WithOne(c => c.Color)
              .HasForeignKey("ColorId")
              .IsRequired();

            modelBuilder.Entity<Color>()
              .HasMany(c => c.ColorLocales)
              .WithOne(cl => cl.Color)
              .HasForeignKey("ColorId")
              .IsRequired();

            modelBuilder.Entity<ColorLocale>()
             .HasOne(cl => cl.Language)
             .WithMany(l => l.ColorLocales)
             .HasForeignKey("LanguageId")
             .IsRequired();

            modelBuilder.Entity<Currency>()
              .HasMany<Car>()
              .WithOne(c => c.Currency)
              .HasForeignKey("CurrencyId")
              .IsRequired();

            modelBuilder.Entity<Transmission>()
              .HasMany<Car>()
              .WithOne(c => c.Transmission)
              .HasForeignKey("TransmissionId")
              .IsRequired();

            modelBuilder.Entity<Transmission>()
             .HasMany(t => t.TransmissionLocales)
             .WithOne(tl => tl.Transmission)
             .HasForeignKey("TransmissionId")
             .IsRequired();

            modelBuilder.Entity<TransmissionLocale>()
           .HasOne(tl => tl.Language)
           .WithMany(l => l.TransmissionLocales)
           .HasForeignKey("LanguageId")
           .IsRequired();

            modelBuilder.Entity<Year>()
              .HasMany<Car>()
              .WithOne(c => c.Year)
              .HasForeignKey("YearId")
              .IsRequired();

            modelBuilder.Entity<Car>()
             .HasMany(c => c.Images)
             .WithOne()
             .HasForeignKey("CarId")
             .IsRequired();

            modelBuilder.Entity<AutoSalon>()
            .HasMany(s => s.Cars)
            .WithOne(c => c.AutoSalon)
            .HasForeignKey("AutoSalonId");

            modelBuilder.Entity<AutoSalon>()
           .HasMany(s => s.AutoSalonLocales)
           .WithOne(sl => sl.AutoSalon)
           .HasForeignKey("AutoSalonId")
           .IsRequired();

            modelBuilder.Entity<AutoSalonLocale>()
           .HasOne(sl => sl.Language)
           .WithMany(l => l.AutoSalonLocales)
           .HasForeignKey("LanguageId")
           .IsRequired();

            modelBuilder.Entity<Feature>()
           .HasMany(f => f.Cars)
           .WithMany(c => c.Features);

            modelBuilder.Entity<Feature>()
           .HasMany(f => f.FeatureLocales)
           .WithOne(fl => fl.Feature)
           .HasForeignKey("FeatureId");

            modelBuilder.Entity<FeatureLocale>()
           .HasOne(fl => fl.Language)
           .WithMany(l => l.FeatureLocales)
           .HasForeignKey("LanguageId");
            #endregion
        }

        public DbSet<User> Users => Set<User>();
    }
}
