using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GeoIpLite
{
    internal class GeoFlagLoader
    {
        public ImageList flgImgList {  get; set; }

        public GeoFlagLoader()
        {
            flgImgList = new ImageList();
            // original flag size 29x23px with 4px transparent margin each side
            // the png size can be customised for the project needs - feel free
            flgImgList.ImageSize = new Size(32, 24); 
        }

        private void AddImage(string coutryCode)
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(FlagResources));
        }
    }
}
