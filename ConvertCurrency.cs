using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace LexiconMiniProject03
{
    public static class CurrencyConverter
    {
        private static string xmlUrl = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";
        private static Envelope envelope;

        public static void UpdateExchangeRates()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Envelope));
                using (XmlReader xmlReader = XmlReader.Create(xmlUrl))
                {
                    envelope = (Envelope)serializer.Deserialize(xmlReader);
                }
                Console.WriteLine("Exchange rates updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update exchange rates: {ex.Message}");
                envelope = null;
            }
        }

        public static bool TryConvertFromUSD(decimal amountUSD, string targetCurrency, out decimal convertedAmount)
        {
            convertedAmount = -1;

            if (envelope == null || envelope.Cube == null || envelope.Cube.Cube1 == null)
            {
                Console.WriteLine("Exchange rate data is not available.");
                return false;
            }

            decimal usdToEurRate = GetRate("USD");
            if (usdToEurRate <= 0)
            {
                Console.WriteLine("Failed to get USD to EUR conversion rate.");
                return false;
            }

            decimal amountEUR = amountUSD / usdToEurRate;

            if (targetCurrency.Equals("EUR", StringComparison.OrdinalIgnoreCase))
            {
                convertedAmount = amountEUR;
                return true;
            }
            decimal targetRate = GetRate(targetCurrency);
            if (targetRate <= 0)
            {
                Console.WriteLine($"Failed to get EUR to {targetCurrency} conversion rate.");
                return false;
            }

            convertedAmount = amountEUR * targetRate;
            return true;
        }

        private static decimal GetRate(string currencyCode)
        {
            foreach (var cubeTime in envelope.Cube.Cube1)
            {
                foreach (var cube in cubeTime.Cube)
                {
                    if (cube.Currency.Equals(currencyCode, StringComparison.OrdinalIgnoreCase))
                    {
                        return cube.Rate;
                    }
                }
            }

            Console.WriteLine($"Currency code {currencyCode} not found in the exchange rate data.");
            return 0;
        }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://www.gesmes.org/xml/2002-08-01")]
    public class Envelope
    {
        [XmlElement(ElementName = "Cube", Namespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref")]
        public CubeWrapper Cube { get; set; }
    }

    public class CubeWrapper
    {
        [XmlElement(ElementName = "Cube", Namespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref")]
        public List<CubeTime> Cube1 { get; set; }
    }

    public class CubeTime
    {
        [XmlAttribute("time")]
        public string Time { get; set; }

        [XmlElement(ElementName = "Cube", Namespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref")]
        public List<Cube> Cube { get; set; }
    }

    public class Cube
    {
        [XmlAttribute("currency")]
        public string Currency { get; set; }

        [XmlAttribute("rate")]
        public decimal Rate { get; set; }
    }
}


