using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspReactTestApp.Entities.Abstract;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace TurboazFetching.Entities
{
    public class Language : IEntity
    {
        public int Id { get; set; }
        public string LanguageName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;


        public List<ColorLocale>? ColorLocales { get; set; }
        public List<RegionLocale>? RegionLocales { get; set; }
        public List<MarketLocale>? MarketLocales { get; set; }
        public List<FeatureLocale>? FeatureLocales { get; set; }
        public List<FuelTypeLocale>? FuelTypeLocales { get; set; }
        public List<CategoryLocale>? CategoryLocales { get; set; }
        public List<GearTypeLocale>? GearTypeLocales { get; set; }
        public List<AutoSalonLocale>? AutoSalonLocales { get; set; }
        public List<TransmissionLocale>? TransmissionLocales { get; set; }
    }
}
