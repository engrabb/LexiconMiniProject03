using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconMiniProject03
{
    public class Office
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string CurrencyCode { get; set; }
        public List<Asset> Assets { get; set; }

        public Office() { }
        public Office(string name, string currencyCode)
        {
            Id = Id;
            Name = name;
            CurrencyCode = currencyCode;
        }
    }
}
