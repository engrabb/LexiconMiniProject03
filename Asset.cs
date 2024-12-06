using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconMiniProject03
{
    public abstract class Asset
    {
        public Asset() { }
        public int Id { get; set; }
        public string Model { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PriceUSD { get; set; } 

        public int OfficeId { get; set; }
        public Office OfficeLocation { get; set; }

        public Asset(string model, DateTime purchaseDate, decimal priceUSD, Office office)
        {

            Model = model;
            PurchaseDate = purchaseDate;
            PriceUSD = priceUSD;
            OfficeLocation = office;
        }

        public bool IsEndOfLife()
        {
            return (DateTime.Now - PurchaseDate).TotalDays >= 3 * 365;
        }

        public bool IsNearEndOfLife(int monthsBeforeEnd = 3)
        {
            return (DateTime.Now - PurchaseDate).TotalDays >= 3 * 365 - monthsBeforeEnd * 30;
        }

        public abstract string GetAssetType();

    }
    public class Computer : Asset
    {
        public Computer()
        {
        }
        public Computer(string model, DateTime purchaseDate, decimal priceUSD, Office office)
            : base(model, purchaseDate, priceUSD, office) { }

        public override string GetAssetType()
        {
            return "Computer";
        }
    }

    public class Phone : Asset
    {
        public Phone() { }
        public Phone(string model, DateTime purchaseDate, decimal priceUSD, Office office)
            : base(model, purchaseDate, priceUSD, office) { }

        public override string GetAssetType()
        {
            return "Phone";
        }
    }

}
