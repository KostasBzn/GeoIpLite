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

        public string GetIpInf(string IPA)
        {
            try
            {
                if (IPAddress.TryParse(IPA, out var ipAddress))
                {
                    if (IPAddress.IsLoopback(ipAddress)) // we check if the ip is 127.0.0.1 (localhost)
                        return $"{IPA}:LocalHost:XY"; // XY is a generic grey flag I created if there is no flag available.

                    var bytes = ipAddress.GetAddressBytes(); // we check for local network ip
                    if (bytes[0] == 10 ||
                        bytes[0] == 172 && bytes[1] >= 16 && bytes[1] <= 31 ||
                        bytes[0] == 192 && bytes[1] == 168)
                        return $"{IPA}:LocalNetwork:XY";
                }

                var country = _databaseReader.Country(IPA);
                var countryName = country.Country.Name ?? "Unknown";
                var countryCode = (country.Country.IsoCode ?? "XY").ToUpper();

                return $"{IPA}:{countryName}:{countryCode}";
            }
            catch
            {
                return $"{IPA}:N/A:XY";
            }
        }

        public void Dispose()
        {
            _databaseReader.Dispose();
            FlagImageList.Dispose();
        }
    }
}
