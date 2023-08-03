using AspReactTestApp.Entities.Concrete.CarRelated;
using TurboazFetching.Entities;

namespace AspReactTestApp.Dto
{
    public class CarDto
    {
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public ushort EngineVolume { get; set; }
        public int Price { get; set; }
        public string Currency { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public int Mileage { get; set; }
        public string MileageType { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;
    }
}
