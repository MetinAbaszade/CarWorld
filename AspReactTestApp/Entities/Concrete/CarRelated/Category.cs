using AspReactTestApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboazFetching.Entities;

namespace AspReactTestApp.Entities.Concrete.CarRelated
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public List<CategoryLocale>? CategoryLocales { get; set; }

        public Category() { }
    }
}
