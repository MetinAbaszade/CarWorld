using AspReactTestApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspReactTestApp.Entities.Concrete.CarRelated
{
    public class FuelType : IEntity
    {
        public int Id { get; set; }

        public List<FuelTypeLocale>? FuelTypeLocales { get; set; } 

        public FuelType() { }
    }
}
