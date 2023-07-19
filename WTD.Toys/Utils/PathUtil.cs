using System.Collections.ObjectModel;
using MintPlayer.PlatformBrowser;

namespace WTD.Toys.Utils
{
    internal class PathUtil
    {
        public static ReadOnlyCollection<Browser> FindInstalledBrowser()
        {
            var browsers = PlatformBrowser.GetInstalledBrowsers();

            return browsers;
        }
    }
}