using AspReactTestApp.Entities.Abstract;
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
        public Brand Brand { get; set; }
        public Model Model { get; set; }
        public Year Year { get; set; }
        public Region Region { get; set; }
        public DateTime LastUpdated { get; set; }
        public ushort EngineVolume { get; set; }
        public ushort HorsePower { get; set; }
        public Fueltype Fueltype { get; set; }
        public Transmission Transmission { get; set; }
        public Category Category { get; set; }
        public int Mileage { get; set; }
        public Color Color { get; set; }
        public int Price { get; set; }
        public Currency Currency { get; set; }
        public bool CreditAvailable { get; set; }
        public bool BarterAvailable { get; set; }
        public string Comment { get; set; }
        public AutoSalon AutoSalon { get; set; }
        public List<Image> Images { get; set; }
        public List<Feature> Features { get; set; }

        public Car() { }

        public Car(int id, Brand brand, Model model, Year year,
                   Region region, ushort engineVolume, ushort horsePower,
                   Fueltype fueltype, Transmission transmission, int mileage,
                   int price, Currency currency, bool creditAvailable, bool barterAvailable,
                   string comment, List<Image> images, Category category, Color color)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Year = year;
            Region = region;
            EngineVolume = engineVolume;
            HorsePower = horsePower;
            Fueltype = fueltype;
            Transmission = transmission;
            Mileage = mileage;
            Price = price;
            Currency = currency;
            CreditAvailable = creditAvailable;
            BarterAvailable = barterAvailable;
            Comment = comment;
            Images = images;
            Category = category;
            Color = color;
        }
    }
}
