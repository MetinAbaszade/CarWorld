using AspReactTestApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspReactTestApp.Entities.Concrete.CarRelated
{
    public class Year : IEntity
    {
        public int Id { get; set; }
        public ushort Value { get; set; }

        public Year() { }

        public Year(ushort value)
        {
            Value = value;
        }
    }
}
