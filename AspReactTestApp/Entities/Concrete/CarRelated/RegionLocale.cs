using AspReactTestApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboazFetching.Entities;

namespace AspReactTestApp.Entities.Concrete.CarRelated
{
    public class RegionLocale : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Region Region { get; set; }
        public Language Language { get; set; }

        public RegionLocale() { }

    }
}
