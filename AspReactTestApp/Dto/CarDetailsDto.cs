namespace AspReactTestApp.Dto
{
    public class CarDetailsDto
    {
        public int Price { get; set; }
        public int Mileage { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Market { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Category { get; set; }
        public string FuelType { get; set; }
        public string GearType { get; set; }
        public string Currency { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public ushort HorsePower { get; set; }
        public ushort? SeatCount { get; set; }
        public string OwnerNumber { get; set; } = string.Empty;
        public string MileageType { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ushort EngineVolume { get; set; }
        public bool CreditAvailable { get; set; }
        public bool BarterAvailable { get; set; }
        public List<string> Images { get; set; } = new();
        public List<string> Features { get; set; } = new();
        public string AutoSalonTitle { get; set; } = string.Empty;
        public string AutoSalonNumber { get; set; } = string.Empty;
        public string AutoSalonLogoUrl { get; set; } = string.Empty;
        public string AutoSalonCoverUrl { get; set; } = string.Empty;
        public string AutoSalonLocationUrl { get; set; } = string.Empty;
        public string AutoSalonSlogan { get; set; } = string.Empty;
        public string AutoSalonDescription { get; set; } = string.Empty;
        public string AutoSalonLocation { get; set; } = string.Empty;
    }
}
