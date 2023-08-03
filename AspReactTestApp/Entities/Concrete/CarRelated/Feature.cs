using AspReactTestApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspReactTestApp.Entities.Concrete.CarRelated
{
    public class Feature : IEntity
    {
        public int Id { get; set; }

        public List<Car>? Cars { get; set; }
        public List<FeatureLocale>? FeatureLocales { get; set; } 

        public Feature() { }
    }
}
