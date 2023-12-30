using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using MintPlayer.PlatformBrowser;
using SmartExtensionMethods;

namespace WinToys.Utils
{
    internal static class PathUtil
    {
        public static async Task<ReadOnlyCollection<Browser>> FindInstalledBrowser()
        {
            var browsers = await PlatformBrowser.GetInstalledBrowsers();

            return browsers;
        }

        public static string MkDir(this string path)
        {
            var dir = Path.GetDirectoryName(path);
            
            if (!dir.IsNullOrEmpty())
                Directory.CreateDirectory(dir);

            return path;
        }
    }
}