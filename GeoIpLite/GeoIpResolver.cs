using MaxMind.GeoIP2;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace GeoIpLite
{
    public class GeoIpResolver : IDisposable
    {
        private readonly DatabaseReader _databaseReader;
        public ImageList FlagImageList { get; }

        public GeoIpResolver()
        {
            // Initialize image list
            FlagImageList = GeoFlagLoader.LoadFlags();

            // Database resource: https://db-ip.com/db/download/ip-to-country-lite *thanks guys*
            using (var ms = new MemoryStream(Properties.Resources.dbip_country_lite_2025_11))
            {
                _databaseReader = new DatabaseReader(ms);  // we load the database
            }
        }

        public (string Country, string CountryCode) GetIpInf(string IPA)
        {
            try
            {
                // check if the ip is valud in first place
                if (!IPAddress.TryParse(IPA, out var ipAddress))
                    return ("N/A", "XY"); // XY us a grey flag I made for unknown or N/A stuff

                // needs refactor to handle ipv6

               

                var country = _databaseReader.Country(IPA);
                var countryName = country.Country.Name ?? "Unknown";
                var countryCode = (country.Country.IsoCode ?? "XY").ToUpper();

                return (countryName, countryCode);
            }
            catch
            {
                return ("N/A", "XY");
            }
        }

        public void Dispose()
        {
            _databaseReader.Dispose();
            FlagImageList.Dispose();
        }
    }
}
