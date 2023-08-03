using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboazFetching.Entities
{
    public class Market
    {
        public int Id { get; set; }

        public List<MarketLocale>? MarketLocales { get; set; }

        public Market() { }
    }
}
