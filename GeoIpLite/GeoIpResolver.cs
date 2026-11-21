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
            using (var ms = new MemoryStream(Properties.Resources.dbip_country_lite_2026_11))
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

                // I guess we have to resolve the ipv4 from ipv6 to check if is localNetwork or Localhost in ipv6 format
                if (ipAddress.IsIPv4MappedToIPv6)
                    ipAddress = ipAddress.MapToIPv4();

                // is localhost
                if (IPAddress.IsLoopback(ipAddress))
                    return ("LocalHost", "XY");

                // is private network
                // i will do it later

                // time to look up the database
                if(_databaseReader.TryCountry(ipAddress, out var country) && country != null)
                {
                    var countryName = country.Country.Name ?? "Unknown";
                    var countryCode = (country.Country.IsoCode ?? "XY").ToUpperInvariant();
                    return (countryName, countryCode);
                }
                // well it didnt work, return something
                return ("N/A", "XY");
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
            GC.SuppressFinalize(this);
        }
    }
}
