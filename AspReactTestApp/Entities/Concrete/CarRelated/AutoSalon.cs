using AspReactTestApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspReactTestApp.Entities.Concrete.CarRelated
{
    public class AutoSalon : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? LocationUrl { get; set; }
        public string Number { get; set; }
        public string LogoUrl { get; set; }
        public string CoverUrl { get; set; }
        public List<AutoSalonLocale> AutoSalonLocales { get; set; }

        public AutoSalon() { }

        public ICollection<Car> Cars { get; set; }
    }
}
