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
            var list = new ImageList
            {
                ColorDepth = ColorDepth.Depth32Bit,
                ImageSize = new Size(32, 24),
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