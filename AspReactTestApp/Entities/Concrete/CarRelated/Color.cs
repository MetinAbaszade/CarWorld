using AspReactTestApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspReactTestApp.Entities.Concrete.CarRelated
{
    public class Color : IEntity
    {
        public int Id { get; set; }
        public List<ColorLocale> ColorLocales { get; set; } = new();

        public Color() { }
    }
}
