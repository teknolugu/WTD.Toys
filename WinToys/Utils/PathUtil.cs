using System.Collections.ObjectModel;
using MintPlayer.PlatformBrowser;

namespace WinToys.Utils
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