using AspReactTestApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboazFetching.Entities;

namespace AspReactTestApp.Entities.Concrete.CarRelated
{
    public class ColorLocale : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Color Color { get; set; }
        public Language Language { get; set; }

        public ColorLocale() { }
    }
}
