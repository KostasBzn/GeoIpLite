using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GeoIpLite
{
    internal class GeoFlagLoader
    {
        public ImageList FlgImgList {  get; set; }

        public GeoFlagLoader()
        {
            FlgImgList = new ImageList();
            // original flag size 29x23px with 4px transparent margin each side
            // the png size can be customised for the project needs - feel free
            FlgImgList.ImageSize = new Size(32, 24);
            FlgImgList.TransparentColor = Color.Transparent;
            LoadAllFlags();
        }

        private void LoadAllFlags()
        {
            var resources = new ComponentResourceManager(typeof(FlagResources));

            foreach (DictionaryEntry entry in resources.GetResourceSet(System.Globalization.CultureInfo.CurrentUICulture, true, true))
            {
                var countryCode = entry.Key.ToString();

                if (resources.GetObject(countryCode) is Image flag)
                    FlgImgList.Images.Add(countryCode, flag);
            }
        }
    }
}
