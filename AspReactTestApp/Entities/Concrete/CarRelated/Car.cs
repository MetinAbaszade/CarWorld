using AspReactTestApp.Entities.Abstract;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboazFetching.Entities;

namespace AspReactTestApp.Entities.Concrete.CarRelated
{
    public class Car : IEntity
    {
        public int Id { get; set; }
        public int? OwnerId { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int ReleaseYearId { get; set; }
        public int ColorId { get; set; }
        public int MarketId { get; set; }
        public int? RegionId { get; set; }
        public int GearTypeId { get; set; }
        public int CategoryId { get; set; }
        public int FueltypeId { get; set; }
        public int CurrencyId { get; set; }
        public int? AutoSalonId { get; set; }
        public int TransmissionId { get; set; }
        public ushort EngineVolume { get; set; }
        public ushort HorsePower { get; set; }
        public DateTime LastUpdated { get; set; }
        public string? Description { get; set; }
        public int Mileage { get; set; }
        public int MileageTypeId { get; set; }
        public int Price { get; set; }
        public ushort? SeatCount { get; set; }
        public bool CreditAvailable { get; set; }
        public bool BarterAvailable { get; set; }
        public List<Image> Images { get; set; } = new();
        public List<Feature> Features { get; set; } = new();

        public User? Owner { get; set; }
        public Brand? Brand { get; set; }
        public Model? Model { get; set; }
        public Year? Year { get; set; }
        public Color? Color { get; set; }
        public Region? Region { get; set; }
        public Market? Market { get; set; }
        public GearType? GearType { get; set; }
        public Category? Category { get; set; }
        public FuelType? Fueltype { get; set; }
        public Currency? Currency { get; set; }
        public MileageType? MileageType { get; set; }
        public Transmission? Transmission { get; set; }
        public AutoSalon? AutoSalon { get; set; }

        public Car() { }
    }
}
