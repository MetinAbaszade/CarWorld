﻿using AspReactTestApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspReactTestApp.Entities.Concrete.CarRelated
{
    public class Currency : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Currency() { }

        public Currency(string name)
        {
            Name = name;
        }
    }
}
