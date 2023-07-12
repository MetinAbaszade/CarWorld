using AspReactTestApp.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspReactTestApp.Entities.Concrete.CarRelated
{
    public class Brand : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Model> Models { get; set; } = new();

        public Brand() { }

        public Brand(string name)
        {
            Name = name;
        }
    }
}
