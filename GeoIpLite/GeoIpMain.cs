using MaxMind.GeoIP2;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
namespace GeoIpLite
{
    public class GeoIpMain
    {
        private DatabaseReader _databaseReader;
        public ImageList flagImageList;

        public GeoIpMain()
        {
            var getFlags = new GeoFlagLoader(); // we intialise the flag list
            flagImageList = getFlags.flgImgList;

            // Database resource: https://db-ip.com/db/download/ip-to-country-lite *thanks guys*
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("GeoIpLite.dbip-country-lite-2025-11.mmdb"))
            {
                _databaseReader = new DatabaseReader(stream); // we load the database
            }
        }

        public string GetIpInf(string ipa)
        {
            try
            {
                if (IPAddress.TryParse(ipa, out var ipAddress)) // we check if the ip is 127.0.0.1 (localhost)
                {
                    if(IPAddress.IsLoopback(ipAddress))
                    {
                        return $"{ipa}:LocalHost:XY"; // XY is a generic grey flag I created if there is no flag available.
                    }

                    var ipBytes = ipAddress.GetAddressBytes(); // we check for local network ip
                    if (ipBytes[0] == 10 || (ipBytes[0] == 172 && ipBytes[1] >= 16 && ipBytes[1] <= 31) || (ipBytes[0] == 192 && ipBytes[1] == 168))
                    {
                        return $"{ipa}:LocalNetwork:XY";
                    }
                }

                var country = _databaseReader.Country(ipa);
                var countryName = country.Country.Name ?? "Unknown";
                var countryCode = (country.Country.IsoCode ?? "XY").ToUpper();
                return $"{ipa}:{countryName}:{countryCode}";

            }
            catch
            {
                return $"{ipa}:N/A:XY";
            }
        }
    }

    
}
