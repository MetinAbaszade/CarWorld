using AspReactTestApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspReactTestApp.Entities.Concrete.CarRelated
{
    public class Region : IEntity
    {
        public int Id { get; set; }

        public List<RegionLocale>? RegionLocales { get; set; }

        public Region() { }
    }
}
