using AspReactTestApp.Entities.Concrete;
using AspReactTestApp.Entities.Concrete.CarRelated;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TurboazFetching.Entities;

namespace AspReactTestApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext() { }

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
       // optionsBuilder.LogTo(message => Console.WriteLine("\n\n\n------------------ New Request ------------------\n\n" + message));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region SettingRelations

        modelBuilder.Entity<User>()
         .HasMany<Car>()
         .WithOne(c => c.Owner)
         .HasForeignKey(c => c.OwnerId)
         .IsRequired(false);

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

        modelBuilder.Entity<Car>()
          .HasOne(c => c.MileageType)
          .WithMany()
          .HasForeignKey(c => c.MileageTypeId)
          .IsRequired()
          .OnDelete(DeleteBehavior.Cascade);

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
        .HasForeignKey(cl => cl.LanguageId)
        .OnDelete(DeleteBehavior.Restrict)
        .IsRequired();

        modelBuilder.Entity<Market>()
          .HasMany<Car>()
          .WithOne(c => c.Market)
          .HasForeignKey(c => c.MarketId)
          .OnDelete(DeleteBehavior.Cascade)
          .IsRequired();

        modelBuilder.Entity<Market>()
         .HasMany(m => m.MarketLocales)
         .WithOne(ml => ml.Market)
         .HasForeignKey("MarketId")
         .OnDelete(DeleteBehavior.Restrict)
         .IsRequired();

        modelBuilder.Entity<MarketLocale>()
        .HasOne(ml => ml.Language)
        .WithMany(l => l.MarketLocales)
        .HasForeignKey(ml => ml.LanguageId)
        .OnDelete(DeleteBehavior.Restrict)
        .IsRequired();

        modelBuilder.Entity<Region>()
          .HasMany<Car>()
          .WithOne(c => c.Region)
          .HasForeignKey(c => c.RegionId)
          .IsRequired(false);

        modelBuilder.Entity<Region>()
         .HasMany(r => r.RegionLocales)
         .WithOne(rl => rl.Region)
         .HasForeignKey("RegionId")
         .IsRequired();

        modelBuilder.Entity<RegionLocale>()
        .HasOne(rl => rl.Language)
        .WithMany(l => l.RegionLocales)
        .HasForeignKey(rl => rl.LanguageId)
        .OnDelete(DeleteBehavior.Restrict)
        .IsRequired();

        modelBuilder.Entity<FuelType>()
          .HasMany<Car>()
          .WithOne(c => c.Fueltype)
          .HasForeignKey(c => c.FueltypeId)
          .IsRequired();

        modelBuilder.Entity<FuelType>()
          .HasMany(ft => ft.FuelTypeLocales)
          .WithOne(ftl => ftl.FuelType)
          .HasForeignKey("FueltypeId")
          .IsRequired();

        modelBuilder.Entity<FuelTypeLocale>()
         .HasOne(ftl => ftl.Language)
         .WithMany(l => l.FuelTypeLocales)
         .HasForeignKey(ftl => ftl.LanguageId)
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
         .HasForeignKey(c => c.LanguageId)
         .IsRequired();

        modelBuilder.Entity<Currency>()
          .HasMany<Car>()
          .WithOne(c => c.Currency)
          .HasForeignKey(c => c.CurrencyId)
          .IsRequired();

        modelBuilder.Entity<GearType>()
          .HasMany<Car>()
          .WithOne(c => c.GearType)
          .HasForeignKey(c => c.GearTypeId)
          .IsRequired();

        modelBuilder.Entity<GearType>()
         .HasMany(gt => gt.GearTypeLocales)
         .WithOne(gtl => gtl.GearType)
         .HasForeignKey("GearTypeId")
         .IsRequired();

        modelBuilder.Entity<GearTypeLocale>()
       .HasOne(gtl => gtl.Language)
       .WithMany(l => l.GearTypeLocales)
       .HasForeignKey(gtl => gtl.LanguageId)
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
       .HasForeignKey(tl => tl.LanguageId)
       .IsRequired();

        modelBuilder.Entity<Year>()
          .HasMany<Car>()
          .WithOne(c => c.Year)
          .HasForeignKey(c => c.ReleaseYearId)
          .IsRequired();

        modelBuilder.Entity<Car>()
         .HasMany(c => c.Images)
         .WithOne()
         .HasForeignKey("CarId")
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AutoSalon>()
        .HasMany(s => s.Cars)
        .WithOne(c => c.AutoSalon)
        .HasForeignKey(c => c.AutoSalonId)
        .IsRequired(false); ;

        modelBuilder.Entity<AutoSalon>()
       .HasMany(s => s.AutoSalonLocales)
       .WithOne(sl => sl.AutoSalon)
       .HasForeignKey("AutoSalonId")
       .IsRequired();

        modelBuilder.Entity<AutoSalonLocale>()
       .HasOne(sl => sl.Language)
       .WithMany(l => l.AutoSalonLocales)
       .HasForeignKey(sl => sl.LanguageId)
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
       .HasForeignKey(fl => fl.LanguageId);
        #endregion
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Year> Years { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Market> Markets { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<FuelType> FuelTypes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<GearType> GearTypes { get; set; }
    public DbSet<MileageType> MileageTypes { get; set; }
    public DbSet<AutoSalon> AutoSalons { get; set; }
    public DbSet<ColorLocale> ColorLocales { get; set; }
    public DbSet<Transmission> Transmissions { get; set; }
    public DbSet<RegionLocale> RegionLocales { get; set; }
    public DbSet<MarketLocale> MarketLocales { get; set; }
    public DbSet<FeatureLocale> FeatureLocales { get; set; }
    public DbSet<FuelTypeLocale> FuelTypeLocales { get; set; }
    public DbSet<CategoryLocale> CategoryLocales { get; set; }
    public DbSet<GearTypeLocale> GearTypeLocales { get; set; }
    public DbSet<AutoSalonLocale> AutoSalonLocales { get; set; }
    public DbSet<TransmissionLocale> TransmissionLocales { get; set; }
}
