using MaxMind.GeoIP2;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Net.Sockets;

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
                if (IsLocalNework(ipAddress))
                    return ("LocalNetwork", "XY");

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

        // private network lookup
        private static bool IsLocalNework(IPAddress iPAddress)
        {
            // so what we look here?
            // we are looking if this is an ipv6 that is imposssible to be public ip. 
            // there is no point to check the database
            if (iPAddress.AddressFamily == AddressFamily.InterNetworkV6)
            {
                bool isPrivate = iPAddress.IsIPv6LinkLocal || iPAddress.IsIPv6SiteLocal || IsIPv6UniqueLocal(iPAddress);
                return isPrivate;
            }
            // local network ipv4 cases, at this point its not ipv6
            var bytes = iPAddress.GetAddressBytes();
            bool isLocalNetwork = bytes[0] == 10 // 10.0.0.0/8
                || bytes[0] == 172 && bytes[1] >= 16 && bytes[1] <= 31 // 172.16.0.0/12
                || bytes[0] == 192 && bytes[1] == 168 // 192.168.0.0/16
                || bytes[0] == 169 && bytes[1] == 254 // 169.254.0.0/16 link local
                || bytes[0] == 100 && bytes[1] >= 64 && bytes[1] <= 127; // 100.64.0.0/10 CGNAT
            return isLocalNetwork;
        }

        // if you targer modern .net you dont need that. you can call iPAddress.IsIPv6UniqueLocal
        private static bool IsIPv6UniqueLocal(IPAddress iPAddress)
        {
            byte[] bytes = iPAddress.GetAddressBytes();
            bool isUniqueLocal = bytes[0] == 0xFC || bytes[0] == 0xFD; // Claude helped me on that
            return isUniqueLocal;
        }

        public void Dispose()
        {
            _databaseReader.Dispose();
            FlagImageList.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
