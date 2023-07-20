using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace WinToys.Utils
{
    internal class ProcessUtil
    {
        public static void DirectBrowserSwitch()
        {
            var args = Environment.GetCommandLineArgs().Skip(1).ToList();

            Process.Start(@"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe", args.LastOrDefault("http"));
            Application.Current.Shutdown(0);
        }
    }
}