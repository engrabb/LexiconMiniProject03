using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconMiniProject03
{
    public class Office
    {
        public string Name { get; set; }
        public string CurrencyCode { get; set; }

        public Office(string name, string currencyCode)
        {
            Name = name;
            CurrencyCode = currencyCode;
        }
    }
}
