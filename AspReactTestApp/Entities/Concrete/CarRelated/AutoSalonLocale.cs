using AspReactTestApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboazFetching.Entities;

namespace AspReactTestApp.Entities.Concrete.CarRelated
{
    public class AutoSalonLocale : IEntity
    {
        public int Id { get; set; }
        public string Slogan { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public Language Language { get; set; }
        public AutoSalon AutoSalon { get; set; }

        public AutoSalonLocale() { }
    }
}
