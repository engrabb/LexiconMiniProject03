// See https://aka.ms/new-console-template for more information
using LexiconMiniProject03;

Console.WriteLine("Hello, World!");

AssetTracker assetTracker = new AssetTracker();


Office newYork = new Office("New York", "USD");
Office london = new Office("London", "GBP");
Office berlin = new Office("Berlin", "EUR");
Office sanDiego = new Office("San Diego", "USD");


assetTracker.AddAsset(new Computer("Legion", new DateTime(2021, 1, 15), 2000, sanDiego));
assetTracker.AddAsset(new Computer("Apple", new DateTime(2021, 1, 20), 2000, newYork));
assetTracker.AddAsset(new Computer("Asus", new DateTime(2019, 6, 10), 1200, berlin));
assetTracker.AddAsset(new Phone("iPhone", new DateTime(2022, 1, 5), 800, london));
assetTracker.AddAsset(new Phone("Samsung", new DateTime(2023, 2, 20), 700, sanDiego));
assetTracker.AddAsset(new Phone("Huawei", new DateTime(2022, 1, 1), 700, sanDiego));



CurrencyConverter.UpdateExchangeRates();
assetTracker.DisplayAssets();
