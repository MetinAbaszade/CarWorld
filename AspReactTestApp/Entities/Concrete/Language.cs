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
        public string LanguageName { get; set; }
        public string DisplayName { get; set; }


        public List<ColorLocale> ColorLocales { get; set; }
        public List<RegionLocale> RegionLocales { get; set; }
        public List<FeatureLocale> FeatureLocales { get; set; }
        public List<FueltypeLocale> FueltypeLocales { get; set; }
        public List<CategoryLocale> CategoryLocales { get; set; }
        public List<AutoSalonLocale> AutoSalonLocales { get; set; }
        public List<TransmissionLocale> TransmissionLocales { get; set; }
    }
}
