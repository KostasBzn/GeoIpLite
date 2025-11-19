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

            // Database resource: Source: https://db-ip.com/db/download/ip-to-country-lite *thanks guys*
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("GeoIpLite.dbip-country-lite-2025-11.mmdb"))
            {
                _databaseReader = new DatabaseReader(stream); // we load the database
            }
        }
    }

    
}
