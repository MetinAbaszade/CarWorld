using AspReactTestApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboazFetching.Entities
{
    public class Image : IEntity
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public Image() {}

        public Image(int id, string url)
        {
            Id = id;
            Url = url;
        }
    }
}
