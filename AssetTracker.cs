using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconMiniProject03
{
    public class AssetTracker
    {
        private readonly MyDbContext _context;

        public AssetTracker(MyDbContext context)
        {
            _context = context;
        }
        public AssetTracker() { }

        // Create - CRUD
        public void AddAsset(Asset asset)
        {
            try
            {
                _context.Assets.Add(asset);
                _context.SaveChanges();
                Console.WriteLine("Asset added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the asset: {ex.Message}");
            }
        }
        // Read - CRUD
        public void DisplayAssets()
        {
            try
            {
                var assets = _context.Assets.Include(a => a.OfficeLocation).ToList();
                var sortedAssets = assets
                    .OrderBy(a => a.OfficeLocation.Name)
                    .ThenBy(a => a.PurchaseDate)
                    .ToList();

                if (!assets.Any())
                {
                    Console.WriteLine("No assets found.");
                    return;
                }

                Console.WriteLine($"{"Id",-10}{"Type",-10}{"Model",-20}{"Purchase Date",-15}{"Price",-10}{"Currency",-10}{"Office",-10}");
                Console.WriteLine(new string('-', 80));

                foreach (var asset in sortedAssets)
                {
                    decimal convertedPrice;
                    if (CurrencyConverter.TryConvertFromUSD(asset.PriceUSD, asset.OfficeLocation.CurrencyCode, out convertedPrice))
                    {
                        string color = asset.IsEndOfLife() ? "Red" : asset.IsNearEndOfLife(3) ? "Yellow" : "White";
                        Console.ForegroundColor = color == "Red" ? ConsoleColor.Red :
                                                  color == "Yellow" ? ConsoleColor.Yellow : ConsoleColor.White;

                        Console.WriteLine($"{asset.Id, -10}{asset.GetAssetType(),-10}{asset.Model,-20}{asset.PurchaseDate.ToShortDateString(),-15}{convertedPrice,-10:0.00}{asset.OfficeLocation.CurrencyCode,-10}{asset.OfficeLocation.Name,-10}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"Failed to convert price for {asset.Model} in office {asset.OfficeLocation.Name}.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving assets: {ex.Message}");
            }
        }

        public void DisplayOffices()
        {
            var offices = _context.Offices.Include(o => o.Assets).ToList();

            Console.WriteLine($"{"Office",-20}{"Currency",-10}{"Asset Count",-10}");
            Console.WriteLine(new string('-', 40));

            foreach (var office in offices)
            {
                Console.WriteLine($"{office.Name,-20}{office.CurrencyCode,-10}{office.Assets.Count,-10}");
            }
        }

        // Update - CRUD
        public void UpdateAsset(int id, string newModel, DateTime newPurchaseDate, decimal newPriceUSD)
        {
            try
            {
                if (_context == null)
                {
                    Console.WriteLine("Database context is not initialized.");
                    return;
                }

                // Retrieve the asset by ID
                var asset = _context.Assets.FirstOrDefault(a => a.Id == id);
                if (asset == null)
                {
                    Console.WriteLine("Asset not found.");
                    return;
                }

                // Update the asset properties
                asset.Model = newModel;
                asset.PurchaseDate = newPurchaseDate;
                asset.PriceUSD = newPriceUSD;

                // Save changes to the database
                _context.SaveChanges();

                Console.WriteLine("Asset updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the asset: {ex.Message}");
            }
        }


        // Delete - CRUD
        public void DeleteAsset(int id)
        {
            try
            {
                var asset = _context.Assets.FirstOrDefault(a => a.Id == id);
                if (asset == null)
                {
                    Console.WriteLine("Asset not found.");
                    return;
                }

                _context.Assets.Remove(asset);
                _context.SaveChanges();
                Console.WriteLine("Asset deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the asset: {ex.Message}");
            }
        }


    }
}
