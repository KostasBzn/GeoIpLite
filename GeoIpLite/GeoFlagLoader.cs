using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace GeoIpLite
{
    internal static class GeoFlagLoader
    {
        public static ImageList LoadFlags()
        {
            // original flag size 29x23px with 4px transparent margin each side
            var list = new ImageList
            {
                ColorDepth = ColorDepth.Depth32Bit,
                ImageSize = new Size(32, 24), // the png size can be customised for the project needs - feel free
                TransparentColor = Color.Transparent
            };

            var set = Properties.Resources.ResourceManager
                .GetResourceSet(CultureInfo.InvariantCulture, true, true);

            foreach (DictionaryEntry entry in set)
            {
                if (entry.Value is Image flag)
                    list.Images.Add((string)entry.Key, flag);
            }

            return list;
        }
    }
}