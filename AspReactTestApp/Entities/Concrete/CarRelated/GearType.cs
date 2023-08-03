using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboazFetching.Entities
{
    public class GearType
    {
        public int Id { get; set; }

        public List<GearTypeLocale>? GearTypeLocales { get; set; } 

        public GearType() { }
    }
}
