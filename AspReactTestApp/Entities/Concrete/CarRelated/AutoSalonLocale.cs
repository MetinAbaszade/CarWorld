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
        public int LanguageId { get; set; }
        public string Slogan { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public Language? Language { get; set; }
        public AutoSalon? AutoSalon { get; set; }

        public AutoSalonLocale() { }
    }
}
