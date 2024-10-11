using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconMiniProject03
{
    public class AssetTracker
    {
        private List<Asset> assets = new List<Asset>();

        public void AddAsset(Asset asset)
        {
            assets.Add(asset);
        }

        public void DisplayAssets()
        {
            var sortedAssets = assets
                .OrderBy(a => a.OfficeLocation.Name)
                .ThenBy(a => a.PurchaseDate)
                .ToList();

            Console.WriteLine($"{"Type",-10}{"Model",-20}{"Purchase Date",-15}{"Price",-10}{"Currency",-10}{"Office",-10}");
            Console.WriteLine(new string('-', 80));

            foreach (var asset in sortedAssets)
            {
                decimal convertedPrice;
                if (CurrencyConverter.TryConvertFromUSD(asset.PriceUSD, asset.OfficeLocation.CurrencyCode, out convertedPrice))
                {
                    string color = asset.IsEndOfLife() ? "Red" : asset.IsNearEndOfLife(3) ? "Yellow" : "White";
                    Console.ForegroundColor = color == "Red" ? ConsoleColor.Red :
                                              color == "Yellow" ? ConsoleColor.Yellow : ConsoleColor.White;

                    Console.WriteLine($"{asset.GetAssetType(),-10}{asset.Model,-20}{asset.PurchaseDate.ToShortDateString(),-15}{convertedPrice,-10:0.00}{asset.OfficeLocation.CurrencyCode,-10}{asset.OfficeLocation.Name,-10}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"Failed to convert price for {asset.Model} in office {asset.OfficeLocation.Name}.");
                }
            }
        }
    }
}
