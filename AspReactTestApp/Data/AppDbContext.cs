using AspReactTestApp.Entities.Concrete;
using AspReactTestApp.Entities.Concrete.CarRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TurboazFetching.Entities;

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
              .HasForeignKey(m => m.BrandId)
              .OnDelete(DeleteBehavior.Cascade)
              .IsRequired();

            modelBuilder.Entity<Model>()
              .HasMany<Car>()
              .WithOne(c => c.Model)
              .HasForeignKey(c => c.ModelId)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Model>()
              .HasOne(m => m.BaseModel)
              .WithMany()
              .HasForeignKey(m => m.BaseModelId)
              .IsRequired(false)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Category>()
              .HasMany<Car>()
              .WithOne(c => c.Category)
              .HasForeignKey(c => c.CategoryId)
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
              .HasForeignKey(c => c.RegionId)
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
              .HasForeignKey(c => c.FueltypeId)
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
              .HasForeignKey(c => c.ColorId)
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
              .HasForeignKey(c => c.CurrencyId)
              .IsRequired();

            modelBuilder.Entity<Transmission>()
              .HasMany<Car>()
              .WithOne(c => c.Transmission)
              .HasForeignKey(c => c.TransmissionId)
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
              .HasForeignKey(c => c.YearId)
              .IsRequired();

            modelBuilder.Entity<Car>()
             .HasMany(c => c.Images)
             .WithOne()
             .HasForeignKey("CarId")
             .IsRequired();

            modelBuilder.Entity<AutoSalon>()
            .HasMany(s => s.Cars)
            .WithOne(c => c.AutoSalon)
            .HasForeignKey(c => c.AutoSalonId);

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

        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Fueltype> Fueltypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<AutoSalon> AutoSalons { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ColorLocale> ColorLocales { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }
        public DbSet<RegionLocale> RegionLocales { get; set; }
        public DbSet<FueltypeLocale> FueltypeLocales { get; set; }
        public DbSet<CategoryLocale> CategoryLocales { get; set; }
        public DbSet<AutoSalonLocale> AutoSalonLocales { get; set; }
        public DbSet<TransmissionLocale> TransmissionLocales { get; set; }
        public DbSet<FeatureLocale> FeatureLocales { get; set; }
    }
}
