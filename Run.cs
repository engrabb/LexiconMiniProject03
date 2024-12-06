using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconMiniProject03
{
    public class Run
    {
        bool running = true;
        MyDbContext db = new MyDbContext();
        AssetTracker AssetTracker;
        public Run()
        {
            AssetTracker = new AssetTracker(db);

        }

        public void RunProgram()
        {

            AssetTracker assetTracker = new AssetTracker(db);
            CurrencyConverter.UpdateExchangeRates();
            Console.WriteLine("Welcome to your asset tracker!");

            while (running)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Display Assets.");
                Console.WriteLine("2. Display available offices.");
                Console.WriteLine("3. Register an asset.");
                Console.WriteLine("4. Update an asset.");
                Console.WriteLine("5. Delete an asset.");
                Console.WriteLine("6. Report phone assets per office.");
                Console.WriteLine("7. Report computer assets per office.");
                Console.WriteLine("8. Exit");
                
                
                string choice = Console.ReadLine();

                switch (choice) 
                {
                    case "1": assetTracker.DisplayAssets(); 
                        continue;
                    case "2": assetTracker.DisplayOffices();
                        continue;
                    case "3": UserCreateAsset();
                        continue;
                    case "4": UserUpdateAsset();
                        continue;
                    case "5":
                        AssetTracker.DisplayAssets();
                        Console.Write("Which asset do you want to delete? Delete by id:");
                        int input = int.Parse(Console.ReadLine());
                        DeleteAsset(input);
                        continue;
                    case "6": ReportPhonesByOffice();
                        continue;
                    case "7": ReportComputersByOffice();
                        continue;

                    case "8": running = false;
                        break;
                    default:
                        Console.WriteLine("Try again");
                        continue;
                }

            }
        }

        public void UserUpdateAsset()
        {
            try
            {
                Console.Write("Enter the ID of the asset to update: ");
                if (!int.TryParse(Console.ReadLine(), out int assetId))
                {
                    Console.WriteLine("Invalid ID. Please enter a numeric value.");
                    return;
                }

                Console.Write("Name of the new model: ");
                string newModel = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newModel))
                {
                    Console.WriteLine("Model cannot be empty.");
                    return;
                }

                Console.Write("Enter the new purchase date (YYYY-MM-DD): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime newPurchaseDate))
                {
                    Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
                    return;
                }

                Console.Write("Enter the new price in USD: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal newPriceUSD) || newPriceUSD <= 0)
                {
                    Console.WriteLine("Invalid price. Please enter a positive numeric value.");
                    return;
                }

                
                AssetTracker.UpdateAsset(assetId, newModel, newPurchaseDate, newPriceUSD);

                Console.WriteLine("Asset updated!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
        public void UserCreateAsset()
        {
            try
            {
                var offices = db.Offices.ToList();
                Console.Write("A phone or a computer? ");
                string type = Console.ReadLine().ToLower();

                if (type == "phone" || type == "computer")
                {
                    Console.Write("Name of the model: ");
                    string newModel = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(newModel))
                    {
                        Console.WriteLine("Model cannot be empty.");
                        return;
                    }

                    Console.Write("Enter the new purchase date (YYYY-MM-DD): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out DateTime purchaseDate))
                    {
                        Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
                        return;
                    }

                    Console.Write("Enter the new price in USD: ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal priceUSD) || priceUSD <= 0)
                    {
                        Console.WriteLine("Invalid price. Please enter a positive numeric value.");
                        return;
                    }

                    Console.Write("What office does it belong to? (Berlin, New York, or London): ");
                    string officeInput = Console.ReadLine().ToLower();

                    Office office = offices.FirstOrDefault(o => o.Name.ToLower() == officeInput);

                    if (office == null)
                    {
                        Console.WriteLine("Invalid office. Please choose from Berlin, New York, or London.");
                        return;
                    }

                    if (type == "phone")
                    {
                        Phone newPhone = new Phone(newModel, purchaseDate, priceUSD, office);
                        AssetTracker.AddAsset(newPhone); 
                        db.SaveChanges();
                        Console.WriteLine("Phone asset created!");
                    }
                    else if (type == "computer")
                    {
                        Computer newComputer = new Computer(newModel, purchaseDate, priceUSD, office);
                        AssetTracker.AddAsset(newComputer); 
                        db.SaveChanges();
                        Console.WriteLine("Computer asset created!");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid asset type. Please choose 'phone' or 'computer'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
        public void DeleteAsset(int assetId)
        {
            AssetTracker.DeleteAsset(assetId);
            db.SaveChanges();
        }
        public void ReportPhonesByOffice()
        {
            try
            {
                
                var offices = db.Offices.Include(o => o.Assets).ToList(); 

                Console.WriteLine("Phones by Office:");

                foreach (var office in offices)
                {
                    // Filter phones in the current office
                    var phoneCount = office.Assets.OfType<Phone>().Count();
                    Console.WriteLine($"{office.Name}: {phoneCount} Phones");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching phone data: {ex.Message}");
            }
        }
        public void ReportComputersByOffice()
        {
            try
            {

                var offices = db.Offices.Include(o => o.Assets).ToList();

                Console.WriteLine("Computers by Office:");

                foreach (var office in offices)
                {
                    
                    var computerCount = office.Assets.OfType<Computer>().Count();
                    Console.WriteLine($"{office.Name}: {computerCount} Computers");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching computer data: {ex.Message}");
            }
        }
    }

}



